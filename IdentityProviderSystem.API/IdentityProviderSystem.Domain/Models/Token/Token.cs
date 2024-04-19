namespace IdentityProviderSystem.Domain.Models.Token;

public class Token : IToken, ITokenResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Secret { get; set; }
    public string Value { get; set; }
    public DateTime DateOfExp { get; set; }
}