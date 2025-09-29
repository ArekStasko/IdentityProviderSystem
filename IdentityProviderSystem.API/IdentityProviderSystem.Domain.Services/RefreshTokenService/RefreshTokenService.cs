using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Persistance.Repositories.RefreshTokenRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Domain.Services.RefreshTokenService;

public class RefreshTokenService : IRefreshTokenService
{
    public IRefreshTokenRepository _refreshTokenRepository { get; }
    public ILogger<RefreshTokenService> _logger { get; }
    
    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, ILogger<RefreshTokenService> logger)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _logger = logger;
    }
    
    public Task<Result<IToken>> Generate(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> Validate(string token)
    {
        try
        {
            var tokensResult = await _refreshTokenRepository.Get();
            var tokens = tokensResult.Match<IList<IToken>>(succ => succ, e =>
            {
                _logger.LogError("Fetch tokens from database throw an error: {e}", e);
                throw e;
            });
            var tokenToValidate = tokens.FirstOrDefault(t => t.Value == token);
            if (tokenToValidate == null)
            {
                _logger.LogError("Received token does not exists");
                return new Result<bool>(false);
            }

            if (tokenToValidate.Alive)
                return new Result<bool>(true);
            
            return new Result<bool>(false);
        }
        catch (Exception e)
        {
            _logger.LogError("Validate refresh token method throw an exception: {e}", e);
            return new Result<bool>(false);
        }
    }
}