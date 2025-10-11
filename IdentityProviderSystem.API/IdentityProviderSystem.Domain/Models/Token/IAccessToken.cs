namespace IdentityProviderSystem.Domain.Models.Token;

public interface IAccessToken : IToken
{
    public string Secret { get; set; }
}