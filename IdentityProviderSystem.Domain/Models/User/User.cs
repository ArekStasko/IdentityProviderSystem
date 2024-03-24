namespace IdentityProviderSystem.Domain.Models.User;

public record User : IUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public byte[] Hash { get; set; }
    public byte[] Salt { get; set; }
}