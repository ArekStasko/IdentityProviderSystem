using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.RefreshTokenService;

public interface IRefreshTokenService
{
    public Task<Result<IToken>> Generate(int userId);
    public Task<Result<bool>> Validate(string token);
}