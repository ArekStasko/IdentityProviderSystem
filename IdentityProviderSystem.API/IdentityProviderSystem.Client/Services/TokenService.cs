using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityProviderSystem.Client.DTO;
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

        public async Task<TokenDto> ValidateToken(string token)
        {
            try
            {
                var tokenDto = new TokenDto() { IsTokenValid = false };
                string uri = $"/api/idp-v1/token/checkTokenExp?token={token}";
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId");
                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return tokenDto;
                }

                var userId = int.Parse(userIdClaim.Value);
                var isTokenValid = JsonConvert.DeserializeObject<bool>(body);
                tokenDto.IsTokenValid = isTokenValid;
                tokenDto.UserId = userId;

                return tokenDto;
            }
            catch (Exception e)
            {
                return new TokenDto() { IsTokenValid = false };
            }
        }

    }

}
