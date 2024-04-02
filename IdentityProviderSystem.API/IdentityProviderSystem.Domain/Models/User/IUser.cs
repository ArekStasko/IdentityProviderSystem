namespace IdentityProviderSystem.Domain.Models.User;

public interface IUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Hash { get; set; }
    public Guid Salt { get; set; }
}