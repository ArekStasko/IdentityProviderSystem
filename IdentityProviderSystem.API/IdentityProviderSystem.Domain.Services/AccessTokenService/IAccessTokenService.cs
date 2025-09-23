using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public interface IAccessTokenService
{
    Task<Result<IAccessToken>> Generate(int userId);
    Task<Result<bool>> Refresh(IToken refreshToken);
    Task<Result<double>> Validate(string token);
}