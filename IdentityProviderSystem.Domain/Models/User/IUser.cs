namespace IdentityProviderSystem.Domain.Models.User;

public interface IUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public byte[] Hash { get; set; }
    public byte[] Salt { get; set; }
}