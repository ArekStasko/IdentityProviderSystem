using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public interface ITokenService
{
    Task<Result<IAccessToken>> Get(int userId);
    Task<Result<IAccessToken>> Generate(int userId);
    Task<Result<bool>> RefreshToken(string token);
    Task<Result<bool>> CheckExp(string token);
}