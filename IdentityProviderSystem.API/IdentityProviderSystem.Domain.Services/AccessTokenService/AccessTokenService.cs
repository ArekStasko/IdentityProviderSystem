using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Services.SaltService;
using IdentityProviderSystem.Persistance.Repositories.AccessTokenRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProviderSystem.Domain.Services.TokenService;

public class AccessTokenService : IAccessTokenService
{
    private readonly IAccessTokenRepository _repository;
    private readonly ISaltService _saltService;
    private readonly ILogger<IAccessTokenService> _logger;

    public AccessTokenService(IAccessTokenRepository repository, ISaltService saltService, ILogger<IAccessTokenService> logger)
    {
        _repository = repository;
        _saltService = saltService;
        _logger = logger;
    }
    public async Task<Result<IAccessToken>> Generate(int userId)
    {
        try
        {
            DateTime value = DateTime.UtcNow.AddMinutes(10.0);
            var result = await _saltService.GenerateSalt();

            var secret = result.Match(succ => succ.ToString(), e =>
            {
                _logger.LogError("Generate Access Token service failed while generating new token secret: {e}",e );
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

            IAccessToken token = new AccessToken()
            {
                UserId = userId,
                Secret = secret,
                Value = tokenValue
            };

            var saveResult = await _repository.Create(token);
            return saveResult.Match(succ => new Result<IAccessToken>(succ), e =>
            {
                _logger.LogError("Something went wrong while saving Access token to db: {e}", e);
                return new Result<IAccessToken>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Generate Access Token failed with an exception: {e}", e);
            return new Result<IAccessToken>(e);
        }
    }

    public async Task<Result<double>> Validate(string token)
    {
        try
        {
            var getTokensResult = await _repository.Get();
            var tokens = getTokensResult.Match(succ => succ, e =>
            {
                _logger.LogError("Get Access tokens failed with an exception: {e}", e);
                throw e;
            });
            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                _logger.LogError("Access Token does not contain userId claim");
                return new Result<double>(0);
            }

            var userId = int.Parse(userIdClaim.Value);
            var userToken = tokens.FirstOrDefault(t => t.UserId == userId);
            if (userToken == null)
            {
                _logger.LogError("Access Token for user with Id: {id} expired", userId);
                return new Result<double>(0);
            }
            
            JwtSecurityToken tokenToValidate = new JwtSecurityToken(userToken.Value);
            double timeToExpire = (tokenToValidate.ValidTo - DateTime.UtcNow).TotalSeconds;
            return new Result<double>(timeToExpire);
        }
        catch (Exception e)
        {
            _logger.LogError("Check Access Token Expiration failed with an exception: {e}", e);
            return new Result<double>(e);
        }
    }
}