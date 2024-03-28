using IdentityProviderSystem.Domain.Models.Salt;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.SaltRepository;

public interface ISaltRepository
{
    public Task<Result<bool>> Delete(ISalt salt);
    public Task<Result<bool>> Add(Guid salt);
    public Task<Result<ISalt>> GetCurrent();
}