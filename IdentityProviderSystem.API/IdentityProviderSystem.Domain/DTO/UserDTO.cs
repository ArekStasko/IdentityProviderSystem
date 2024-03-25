namespace IdentityProviderSystem.Domain.DTO;

public record UserDTO
{
    public string Username { get; set; }
    public string Hash { get; set; }
}