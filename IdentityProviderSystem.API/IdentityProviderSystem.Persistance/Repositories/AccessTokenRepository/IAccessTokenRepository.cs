using IdentityProviderSystem.Domain.Models.Token;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.AccessTokenRepository;

public interface IAccessTokenRepository
{
    public Task<Result<bool>> Remove(int Id);
    public Task<Result<IAccessToken>> Create(IAccessToken token);
    public Task<Result<IAccessToken>> Update(IAccessToken token);
    public Task<Result<IAccessToken>> Get(int userId);
    public Task<Result<IList<IAccessToken>>> Get();
}