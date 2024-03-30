namespace IdentityProviderSystem.Domain.Models.Salt;

public class Salt : ISalt
{
    public int Id { get; set; }
    public DateTime DateOfGeneration { get; set; }
    public Guid SaltValue { get; set; } = Guid.Empty;
}