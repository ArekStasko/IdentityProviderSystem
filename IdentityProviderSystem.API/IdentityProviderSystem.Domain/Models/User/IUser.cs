using IdentityProviderSystem.Domain.Models.Token;

namespace IdentityProviderSystem.Domain.Models.User;

public interface IUser
{
    public int Id { get; set; }
    public AccessToken AccessToken { get; set; }
    public string Username { get; set; }
    public string Hash { get; set; }
}