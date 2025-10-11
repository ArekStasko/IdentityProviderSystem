using AutoMapper;
using IdentityProviderSystem.Domain.Services.TokenService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProviderSystem.Controllers.AccessToken;

[Route("api/idp/access-token/[action]")]
[ApiController]
public class AccessTokenController : ControllerBase
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly ILogger<AccessTokenController> _logger;
    
    public AccessTokenController(IAccessTokenService accessTokenService,  ILogger<AccessTokenController> logger)
    {
        _accessTokenService = accessTokenService;
        _logger = logger;
    }
    
    [HttpGet(Name = "[controller]/validate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Validate(string token)
    {
        _logger.LogInformation("Validate token expiration endpoint starts processing");
        var result = await _accessTokenService.Validate(token);
        return result.ToOk();
    }
}