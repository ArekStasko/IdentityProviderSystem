using System.IdentityModel.Tokens.Jwt;
using Coravel.Invocable;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Persistance.Repositories.RefreshTokenRepository;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.JobScheduler.Jobs;

public class RefreshTokenJob : IInvocable
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ILogger<RefreshTokenJob> _logger;

    public RefreshTokenJob(IRefreshTokenRepository refreshTokenRepository, ILogger<RefreshTokenJob> logger)
    {
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
    }
    
    public async Task Invoke()
    {
        try
        {
            var refreshTokens = (await _refreshTokenRepository.Get()).Match(succ => succ, err =>
            {
                _logger.LogError("Refresh token job throws an exception while fetching refresh tokens: {err}", err);
                throw err;
            });
            foreach (IToken refreshToken in refreshTokens)
            {
                JwtSecurityToken jwtRefreshToken = new JwtSecurityToken(refreshToken.Value);
                bool isExpired = jwtRefreshToken.ValidTo < DateTime.UtcNow;
                if (isExpired)
                {
                    var removeTokenResult = await _refreshTokenRepository.Remove(refreshToken.Id);
                    _ = removeTokenResult.Match(succ => succ, err =>
                    {
                        _logger.LogError("Something went wrong while removing expired refresh token: {err}", err);
                        throw err;
                    });
                }
            }
            
        }
        catch (Exception e)
        {
            _logger.LogError("Refresh token job throws an error: {e}", e);
            throw;
        }
    }
}