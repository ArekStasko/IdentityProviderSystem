using IdentityProviderSystem.Persistance.Repositories.SaltRepository;
using LanguageExt;
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
    
    public async Task<Result<string>> GenerateSalt()
    {
        try
        {
            var hash = BCrypt.Net.BCrypt.GenerateSalt();
            if (hash == null)
            {
                return new Result<string>(new Exception("Generated salt is null"));
            }
            var result = await _repository.GetCurrent();
            return result.Match(currentSalt =>
            {
                if (currentSalt.SaltValue == "")return new Result<string>(hash);
                if (currentSalt.SaltValue == hash) return new Result<string>(BCrypt.Net.BCrypt.GenerateSalt());
                return new Result<string>(hash);
            }, e =>
            {
                _logger.LogError("Generate Salt failed with error from salt repository: {e}", e);
                return new Result<string>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Generate Salt failed with an exception: {e}", e);            
            return new Result<string>(e);
        }
    }

    public async Task<Result<string>> GetCurrentSalt()
    {
        try
        {
            var result = await _repository.GetCurrent();
            return result.Match(succ => succ.SaltValue, e =>
            {
                _logger.LogError("Get current salt failed with an exception: {e}", e);
                return new Result<string>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Get current Salt failed with an exception: {e}", e);            
            return new Result<string>(e);
        }
    }
}