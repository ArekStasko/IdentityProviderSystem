using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.SaltService;

public interface ISaltService
{
    public Task<Result<Guid>> GenerateSalt();
    public Task<Result<Guid>> GetCurrentSalt();
}