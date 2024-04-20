using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Castle.Core.Logging;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Services.SaltService;
using IdentityProviderSystem.Persistance.Repositories.TokenRepository;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _repository;
    private readonly ISaltService _saltService;
    private readonly ILogger<ITokenService> _logger;

    public TokenService(ITokenRepository repository, ISaltService saltService, ILogger<ITokenService> logger)
    {
        _repository = repository;
        _saltService = saltService;
        _logger = logger;
    }
    
    public async Task<Result<IToken>> Get(int userId)
    {
        try
        {
            var result = await _repository.Get(userId);
            return result.Match(token => new Result<IToken>(token), e =>
            {
                _logger.LogError("Get token failed with an exception: {e}", e);
                return new Result<IToken>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Get Token by user id failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<IToken>> Generate(int userId)
    {
        try
        {
            var existingTokensResult = await _repository.Get();
            var existingTokens = existingTokensResult.Match(tokens => tokens, e =>
            {
                _logger.LogError("Generate Token service failed while getting tokens: {e}", e );
                throw e;
            });
            var existingToken = existingTokens.FirstOrDefault(t => t.UserId == userId);
            if (existingToken != null) return new Result<IToken>(existingToken);
                
            DateTime value = DateTime.UtcNow.AddMinutes(10.0);
            var result = await _saltService.GenerateSalt();

            var secret = result.Match(succ => succ.ToString(), e =>
            {
                _logger.LogError("Generate Token service failed while generating new token secret: {e}",e );
                throw e;
            });
            
            byte[] bytes = Encoding.ASCII.GetBytes(secret);
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new ("userId", userId.ToString())
                }),
                Expires = value,
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken tokenToWrite = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            var tokenValue = jwtSecurityTokenHandler.WriteToken(tokenToWrite);

            IToken token = new Token()
            {
                UserId = userId,
                Secret = secret,
                Alive = true,
                Value = tokenValue
            };

            var saveResult = await _repository.Create(token);
            return saveResult.Match(succ => new Result<IToken>(succ), e =>
            {
                _logger.LogError("Something went wrong while saving token to db: {e}", e);
                return new Result<IToken>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Generate Token failed with an exception: {e}", e);
            return new Result<IToken>(e);
        }
    }

    public async Task<Result<bool>> RefreshToken(string token)
    {
        try
        {
            var getTokensResult = await _repository.Get();
            var tokens = getTokensResult.Match(succ => succ, e =>
            {
                _logger.LogError("Get tokens failed with an exception: {e}", e);
                throw e;
            });
            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                _logger.LogError("Token does not contain userId claim");
                return new Result<bool>(false);
            }

            var userId = int.Parse(userIdClaim.Value);
            var userToken = tokens.FirstOrDefault(t => t.UserId == userId);
            if (userToken == null)
            {
                _logger.LogError("Token for user with Id: {id} expired", userId);
                return new Result<bool>(false);
            }

            userToken.Alive = true;
            var result = await _repository.Update(userToken);
            return result.Match(_ => new Result<bool>(true), e =>
            {
                _logger.LogError("Update token alive status failed with exception: {e}", e);
                return new Result<bool>(false);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Update token alive status failed with an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<bool>> CheckExp(string token)
    {
        try
        {
            var getTokensResult = await _repository.Get();
            var tokens = getTokensResult.Match(succ => succ, e =>
            {
                _logger.LogError("Get tokens failed with an exception: {e}", e);
                throw e;
            });
            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                _logger.LogError("Token does not contain userId claim");
                return new Result<bool>(false);
            }

            var userId = int.Parse(userIdClaim.Value);
            var userToken = tokens.FirstOrDefault(t => t.UserId == userId);
            if (userToken == null)
            {
                _logger.LogError("Token for user with Id: {id} expired", userId);
                return new Result<bool>(false);
            }
            
            if (userToken.Alive) return new Result<bool>(true);
            
            JwtSecurityToken tokenToValidate = new JwtSecurityToken(userToken.Value);
            bool isExpired = tokenToValidate.ValidTo > DateTime.UtcNow;
            return new Result<bool>(isExpired);
        }
        catch (Exception e)
        {
            _logger.LogError("Check Token Expiration failed with an exception: {e}", e);
            return new Result<bool>(e);
        }
    }
}