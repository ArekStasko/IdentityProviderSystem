namespace IdentityProviderSystem.Domain.Models.Token;

public interface ITokenResponse
{
    public int UserId { get; set; }
    public string Secret { get; set; }
}