using System.Threading.Tasks;
using IdentityProviderSystem.Client.DTO;

namespace IdentityProviderSystem.Client.Services
{

    public interface ITokenService
    {
        Task<TokenDto> ValidateToken(string token);
    }

}
