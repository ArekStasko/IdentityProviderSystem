using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Interfaces;
using IdentityProviderSystem.Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProviderSystem.Controllers.User;

[Route("api/idp-v1/user/[action]")]
[ApiController]
public class UserController
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [HttpPost(Name = "[controller]/register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Register(IUserBaseData registerRequest)
    {
        UserDTO userDto = new UserDTO()
        {
            Username = registerRequest.Username,
            Hash = registerRequest.Hash
        };
        _logger.LogInformation("Register endpoint starts processing");
        var result = await _userService.Register(userDto);
        return result.ToOk();
    }
    
    [HttpPost(Name = "[controller]/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Login(IUserBaseData loginRequest)
    {
        UserDTO userDto = new UserDTO()
        {
            Username = loginRequest.Username,
            Hash = loginRequest.Hash
        };
        _logger.LogInformation("Login endpoint starts processing");
        var result = await _userService.Login(userDto);
        return result.ToOk();
    }
    
    [HttpPost(Name = "[controller]/get-status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetStatus([FromQuery] int id)
    {
        var result = await _userService.GetStatus(id);
        _logger.LogInformation("Get Status endpoint starts processing");
        return result.ToOk();
    }
}