using IdentityProviderSystem.Domain.Models.Salt;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.SaltRepository;

public interface ISaltRepository
{
    public Task<Result<bool>> Delete();
    public Task<Result<bool>> Add(string salt);
    public Task<Result<ISalt>> GetCurrent();
}