using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.SaltService;

public class SaltService : ISaltService
{
    public Task<Result<Guid>> GenerateSalt()
    {
        var uuid = Guid.NewGuid();
        
    }
}