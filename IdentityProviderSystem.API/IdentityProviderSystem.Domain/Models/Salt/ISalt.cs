namespace IdentityProviderSystem.Domain.Models.Salt;

public interface ISalt
{
    public int Id { get; set; }
    public DateTime DateOfGeneration { get; set; }
    public string SaltValue { get; set; }
}