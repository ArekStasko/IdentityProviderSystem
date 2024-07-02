using AutoMapper;
using IdentityProviderSystem.Domain.Services.TokenService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProviderSystem.Controllers.Token;

[Route("api/idp-v1/token/[action]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<TokenController> _logger;
    
    public TokenController(ITokenService tokenService, IMapper mapper,  ILogger<TokenController> logger)
    {
        _tokenService = tokenService;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost(Name = "[controller]/refreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> RefreshToken(string token)
    {
        _logger.LogInformation("Refresh token endpoint starts processing");
        var result = await _tokenService.RefreshToken(token);
        return result.ToOk();
    }
    
    [HttpGet(Name = "[controller]/checkTokenExp")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> CheckTokenExp(string token)
    {
        _logger.LogInformation("Check token expiration endpoint starts processing");
        var result = await _tokenService.CheckExp(token);
        return result.ToOk();
    }
}