using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.SaltService;

public interface ISaltService
{
    public Task<Result<string>> GenerateSalt();
    public Task<Result<string>> GetCurrentSalt();
}