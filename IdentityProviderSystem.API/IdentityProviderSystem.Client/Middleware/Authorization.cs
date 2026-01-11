using System;
using System.Threading.Tasks;
using IdentityProviderSystem.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Client.Middleware
{
    public class Authorization
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public Authorization(RequestDelegate next, ITokenService tokenService, ILogger<Authorization> logger)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            string secretToken = Environment.GetEnvironmentVariable("secretToken");

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                var transformedToken = token.Substring("Bearer ".Length).Trim();
                if(transformedToken == secretToken)
                {
                    await _next(context);
                }
            }

            var tokenValidationResult = await _tokenService.ValidateToken(token);
            if (!tokenValidationResult.IsTokenValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            context.Items["UserId"] = tokenValidationResult.UserId;
            await _next(context);
        }
    }

}
