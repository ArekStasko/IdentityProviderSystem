using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public interface IAccessTokenService
{
    Task<Result<IAccessToken>> Generate(int userId);
    Task<Result<bool>> RefreshToken(string token);
    Task<Result<bool>> CheckExp(string token);
}