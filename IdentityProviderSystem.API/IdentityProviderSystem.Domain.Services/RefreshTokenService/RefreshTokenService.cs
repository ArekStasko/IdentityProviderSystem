using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Services.SaltService;
using IdentityProviderSystem.Persistance.Repositories.RefreshTokenRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProviderSystem.Domain.Services.RefreshTokenService;

public class RefreshTokenService : IRefreshTokenService
{
    public IRefreshTokenRepository _repository { get; }
    public ISaltService _saltService { get; }
    public ILogger<RefreshTokenService> _logger { get; }
    
    public RefreshTokenService(IRefreshTokenRepository repository, ISaltService saltService, ILogger<RefreshTokenService> logger)
    {
        _repository = repository;
        _saltService = saltService;
        _logger = logger;
    }
    
    public async Task<Result<IToken>> Generate(int userId)
    {
        try
        {
            DateTime value = DateTime.UtcNow.AddMinutes(60);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = value,
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken tokenToWrite = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            var tokenValue = jwtSecurityTokenHandler.WriteToken(tokenToWrite);

            IToken token = new RefreshToken()
            {
                Value = tokenValue
            };

            var saveResult = await _repository.Create(token);
            return saveResult.Match(succ => new Result<IToken>(succ), e =>
            {
                _logger.LogError("Something went wrong while saving refresh token to db: {e}", e);
                return new Result<IToken>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Generate Refresh Token failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<bool>> Validate(string token)
    {
        try
        {
            var tokensResult = await _repository.Get();
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
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Validate refresh token method throw an exception: {e}", e);
            return new Result<bool>(false);
        }
    }

    public async Task<Result<int>> GetUserId(string token)
    {
        try
        {
            _logger.LogError("Get user id by refresh token starts processing");
            var refreshTokenResult = await _repository.Get(token);
            return refreshTokenResult.Match(succ => succ.UserId, err =>
            {
                _logger.LogError("Get refresh token throw an error: {e}", err);
                return new Result<int>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Validate refresh token method throw an exception: {e}", e);
            return new Result<int>(e);
        }
    }
}