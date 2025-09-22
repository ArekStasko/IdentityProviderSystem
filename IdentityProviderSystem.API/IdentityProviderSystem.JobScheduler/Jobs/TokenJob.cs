using System.IdentityModel.Tokens.Jwt;
using Coravel.Invocable;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Persistance.Repositories.TokenRepository;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.JobScheduler.Jobs;

public class TokenJob : IInvocable
{
    private readonly ITokenRepository _repository;
    private readonly ILogger<TokenJob> _logger;

    public TokenJob(ITokenRepository repository, ILogger<TokenJob> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task Invoke()
    {
        try
        {
            var result = await _repository.Get();
            var tokens = result.Match<IList<IAccessToken>>(tokens => tokens, e =>
            {
                _logger.LogError("Error occured while fetching tokens in Token Job", e);
                throw e;
            });
            
            foreach (IAccessToken token in tokens)
            {
                if (token.Alive)
                {
                    token.Alive = false;
                    await _repository.Update(token);
                    return;
                }
                
                JwtSecurityToken tokenToValidate = new JwtSecurityToken(token.Value);
                bool isExpired = tokenToValidate.ValidTo < DateTime.UtcNow;
                if (isExpired)
                {
                    var removeTokenResult = await _repository.Remove(token.Id);
                    _ = removeTokenResult.Match(succ => succ, e =>
                    {
                        _logger.LogError("Remove expired token failed with error: {e}", e);
                        throw e;
                    });
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while executing Token Job : {e}", e);
            throw;
        }
    }
}