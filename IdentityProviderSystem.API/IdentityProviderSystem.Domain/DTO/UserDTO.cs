using IdentityProviderSystem.Domain.Interfaces;

namespace IdentityProviderSystem.Domain.DTO;

public record UserDTO : IUserBaseData
{
    public string Username { get; set; }
    public string Password { get; set; }
}