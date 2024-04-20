using IdentityProviderSystem.Domain.Interfaces;

namespace IdentityProviderSystem.Domain.Requests.LoginUser;

public record LoginUser : IUserBaseData
{
    public string Username { get; set; }
    public string Password { get; set; }
}