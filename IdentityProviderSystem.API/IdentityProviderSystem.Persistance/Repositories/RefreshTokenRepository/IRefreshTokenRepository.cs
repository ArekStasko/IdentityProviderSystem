using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository
{
    public Task<Result<bool>> Remove(int Id);
    public Task<Result<IToken>> Create(IToken token);
    public Task<Result<IList<IToken>>> Get();
    Task<Result<IToken?>> GetByUserId(int id);
    public Task<Result<IToken>> Get(string token);
}