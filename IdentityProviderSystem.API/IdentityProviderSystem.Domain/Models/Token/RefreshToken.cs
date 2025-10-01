namespace IdentityProviderSystem.Domain.Models.Token;

public class RefreshToken : IToken
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public bool Alive { get; set; }
    public string Value { get; set; }
}