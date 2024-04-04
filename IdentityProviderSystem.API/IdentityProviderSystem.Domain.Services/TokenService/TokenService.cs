using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public class TokenService : ITokenService
{
    public Task<Result<Token>> Get(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Token>> Generate(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> CheckExp()
    {
        throw new NotImplementedException();
    }
}