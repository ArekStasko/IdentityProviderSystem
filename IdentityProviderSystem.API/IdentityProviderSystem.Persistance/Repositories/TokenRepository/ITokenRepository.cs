using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.TokenRepository;

public interface ITokenRepository
{
    public Task<Result<bool>> Remove(int Id);
    public Task<Result<IToken>> Create(IToken token);
    public Task<Result<IToken>> Update(IToken token);
    public Task<Result<IToken>> Get(int userId);
    public Task<Result<IList<IToken>>> Get();
}