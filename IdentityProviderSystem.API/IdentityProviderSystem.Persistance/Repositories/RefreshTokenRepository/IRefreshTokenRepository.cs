using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository
{
    public Task<Result<bool>> Remove(int Id);
    public Task<Result<IToken>> Create(IToken token);
    public Task<Result<IToken>> Update(IToken token);
    public Task<Result<IList<IToken>>> Get();
    public Task<Result<IToken>> Get(string token);
}