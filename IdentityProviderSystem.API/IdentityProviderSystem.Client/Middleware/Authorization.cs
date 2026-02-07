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
        private readonly ILogger<Authorization> _logger;

        public Authorization(RequestDelegate next, ITokenService tokenService, ILogger<Authorization> logger)
        {
            _next = next;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                _logger.LogWarning("No authorization token provided");
                return;
            }

            string secretToken = Environment.GetEnvironmentVariable("secretToken");

            if (!string.IsNullOrEmpty(token))
            {
                if(token == secretToken)
                {
                    _logger.LogInformation("Successfully authenticated by secret token");
                    await _next(context);
                    return;
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
