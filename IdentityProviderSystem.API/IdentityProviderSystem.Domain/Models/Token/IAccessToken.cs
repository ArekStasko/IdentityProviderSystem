namespace IdentityProviderSystem.Domain.Models.Token;

public interface IAccessToken : IToken
{
    public int UserId { get; set; }
}