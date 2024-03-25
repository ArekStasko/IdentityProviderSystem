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
    public UserController(IUserService userService)
    {
        _userService = userService;
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
        var result = await _userService.Login(userDto);
        return result.ToOk();
    }
    
    [HttpPost(Name = "[controller]/get-status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetStatus([FromQuery] int id)
    {
        var result = await _userService.GetStatus(id);
        return result.ToOk();
    }
}