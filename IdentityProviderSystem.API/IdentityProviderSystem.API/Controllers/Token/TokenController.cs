using AutoMapper;
using IdentityProviderSystem.Domain.Services.TokenService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProviderSystem.Controllers.Token;

[Route("api/idp-v1/token/[action]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<TokenController> _logger;
    
    public TokenController(IAccessTokenService accessTokenService, IMapper mapper,  ILogger<TokenController> logger)
    {
        _accessTokenService = accessTokenService;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost(Name = "[controller]/refreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> RefreshToken(string refreshToken)
    {
        _logger.LogInformation("Refresh token endpoint starts processing");
        var result = await _accessTokenService.Refresh(refreshToken);
        return result.ToOk();
    }
    
    [HttpGet(Name = "[controller]/checkTokenExp")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> CheckTokenExp(string token)
    {
        _logger.LogInformation("Check token expiration endpoint starts processing");
        var result = await _accessTokenService.CheckExp(token);
        return result.ToOk();
    }
}