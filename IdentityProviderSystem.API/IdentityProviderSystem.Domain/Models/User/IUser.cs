namespace IdentityProviderSystem.Domain.Models.User;

public interface IUser
{
    public int Id { get; set; }
    public Token.Token Token { get; set; }
    public string Username { get; set; }
    public string Hash { get; set; }
    public string Salt { get; set; }
}