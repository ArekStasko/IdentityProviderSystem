namespace IdentityProviderSystem.Domain.Models.Token;

public interface ITokenResponse
{
    public string Secret { get; set; }
}