using IdentityProviderSystem.Domain.Interfaces;

namespace IdentityProviderSystem.Domain.Requests.RegisterUser;

public record RegisterUser : IUserBaseData
{
    public string Username { get; set; }
    public string Password { get; set; }
}