using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public interface ITokenService
{
    public Task<Result<Token>> Get(int userId);
    public Task<Result<Token>> Generate(int userId);
    public Task<Result<bool>> CheckExp();
}