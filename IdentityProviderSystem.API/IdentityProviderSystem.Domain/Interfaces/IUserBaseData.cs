namespace IdentityProviderSystem.Domain.Interfaces;

public interface IUserBaseData
{
    public string Username { get; set; }
    public string Password { get; set; }
}