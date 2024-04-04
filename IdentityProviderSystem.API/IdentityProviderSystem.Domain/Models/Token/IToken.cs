namespace IdentityProviderSystem.Domain.Models.Token;

public interface IToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Value { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime DateOfExp { get; set; }
}