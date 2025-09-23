using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.RefreshTokenService;

public class RefreshTokenService : IRefreshTokenService
{
    public Task<Result<IToken>> Generate(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> Validate(string token)
    {
        throw new NotImplementedException();
    }
}