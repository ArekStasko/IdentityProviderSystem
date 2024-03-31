using Coravel.Invocable;
using IdentityProviderSystem.Domain.Services.SaltService;
using IdentityProviderSystem.Persistance.Repositories.SaltRepository;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.JobScheduler.Jobs;

public class SaltJob : IInvocable
{
    private readonly ISaltRepository _repository;
    private readonly ISaltService _saltService;
    private readonly ILogger<SaltJob> _logger;

    public SaltJob(ISaltRepository repository, ISaltService saltService, ILogger<SaltJob> logger)
    {
        _repository = repository;
        _saltService = saltService;
        _logger = logger;
    }
    
    public async Task Invoke()
    {
        try
        {
            var saltServiceResult = await _saltService.GenerateSalt();

            var newSalt = saltServiceResult.Match(succ => succ, 
                e =>
            {
                _logger.LogError("Salt service returns exception: {e}", e);
                throw e;
            });
            
            var deleteResult = await _repository.Delete();
            var isDeleteSucceded = deleteResult.Match(succ => succ, e =>
            {
                _logger.LogError("Error occured while Salt job execution: {e}", e);
                throw e;
            });

            if (isDeleteSucceded)
            {
                var addResult = await _repository.Add(newSalt);
                var isAddSucceded = addResult.Match(succ => succ, e =>
                {
                    _logger.LogError("Error occured while add new salt execution: {e}", e);
                    throw e;
                });

                if (isAddSucceded == false)
                {
                    _logger.LogError("Add new salt failed while executing: {result}", isAddSucceded);
                    throw new Exception("Add new salt failed while executing");
                };
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while Salt job execution: {e}", e);
            throw;
        }
    }
}