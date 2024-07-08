using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            try
            {
                string uri = $"/checkTokenExp?token={token}";
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(body);
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }

}
