namespace IdentityProviderSystem.Client.DTO
{

    public class TokenDto
    {
        public bool IsTokenValid { get; set; }
        public int UserId { get; set; } = -1;
    }

}
