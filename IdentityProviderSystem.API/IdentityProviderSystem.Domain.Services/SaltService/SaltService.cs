using IdentityProviderSystem.Persistance.Repositories.SaltRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Domain.Services.SaltService;

public class SaltService : ISaltService
{
    private readonly ISaltRepository _repository;
    private readonly ILogger<ISaltService> _logger;
    
    public SaltService(ISaltRepository repository, ILogger<ISaltService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<Guid>> GenerateSalt()
    {
        try
        {
            var uuid = Guid.NewGuid();
            var result = await _repository.GetCurrent();
            return result.Match(currentSalt =>
            {
                if (currentSalt.SaltValue == Guid.Empty)return new Result<Guid>(uuid);
                if (currentSalt.SaltValue == uuid) return new Result<Guid>(Guid.NewGuid());
                return new Result<Guid>(uuid);
            }, e =>
            {
                _logger.LogError("Generate Salt failed with error from salt repository: {e}", e);
                return new Result<Guid>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Generate Salt failed with an exception: {e}", e);            
            return new Result<Guid>(e);
        }
    }

    public async Task<Result<Guid>> GetCurrentSalt()
    {
        try
        {
            var result = await _repository.GetCurrent();
            return result.Match(succ => succ.SaltValue, e =>
            {
                _logger.LogError("Get current salt failed with an exception: {e}", e);
                return new Result<Guid>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Get current Salt failed with an exception: {e}", e);            
            return new Result<Guid>(e);
        }
    }
}