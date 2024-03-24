using IdentityProviderSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProviderSystem.Controllers.User;

[Route("api/idp-v1/user/[action]")]
[ApiController]
public class UserController
{
    public UserController()
    {
        
    }
    
    [HttpPost(Name = "[controller]/register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Register(IUserBaseData registerRequest)
    {
    }
    
    [HttpPost(Name = "[controller]/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> Login(IUserBaseData loginRequest)
    {
    }
    
    [HttpPost(Name = "[controller]/get-status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
    public async ValueTask<IActionResult> GetStatus([FromQuery] int id)
    {
    }
}