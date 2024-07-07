using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityProviderSystem.Client.Services
{

    public class TokenService : ITokenService
    {

        private HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateToken(string token)
        {
            throw new System.NotImplementedException();
        }

    }

}
