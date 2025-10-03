using AutoMapper;
using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Interfaces;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProviderSystem.Controllers.User;

[Route("api/idp/user/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IUserService userService,  ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [HttpPost(Name = "[controller]/register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Register(UserDTO registerRequest)
    {
        _logger.LogInformation("Register endpoint starts processing");
        var result = await _userService.Register(registerRequest);
        return result.ToOk();
    }
    
    [HttpPost(Name = "[controller]/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Login(UserDTO loginRequest)
    {
        _logger.LogInformation("Login endpoint starts processing");
        var result = await _userService.Login(loginRequest);
        return result.ToOk();
    }
    
    [HttpPost(Name = "[controller]/refresh-session")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Login(string refreshToken)
    {
        _logger.LogInformation("Login endpoint starts processing");
        var result = await _userService.RefreshSession(refreshToken);
        return result.ToOk();
    }
}