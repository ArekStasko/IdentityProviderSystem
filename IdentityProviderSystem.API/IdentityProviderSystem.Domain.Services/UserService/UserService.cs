using AutoMapper;
using IdentityProviderSystem.Domain.DTO;
using System.IdentityModel.Tokens.Jwt;
using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Domain.Services.SaltService;
using IdentityProviderSystem.Persistance.Repositories.UserRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProviderSystem.Domain.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ISaltService _saltService;
    private readonly IMapper _mapper;
    private readonly ILogger<IUserService> _logger;
    
    public UserService(IUserRepository repository, ISaltService saltService, IMapper mapper, ILogger<IUserService> logger)
    {
        _repository = repository;
        _saltService = saltService;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Register(UserDTO user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var currentSaltResult = await _saltService.GetCurrentSalt();
            var currentSalt = currentSaltResult.Match<Guid>(salt => salt, e =>
            {
                _logger.LogError("Salt service failed while getting current salt: {e}", e);
                return Guid.Empty;
            });
            if (currentSalt == Guid.Empty) return new Result<bool>(new NullReferenceException("There is no current salt"));
            
            var key = currentSalt.ToByteArray();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            
            SecurityToken securityToken;
            _ = tokenHandler.ValidateToken(user.JWT, parameters, out securityToken);
            JwtSecurityToken jwtToken = (JwtSecurityToken)securityToken;

            var pwdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "password");

            if (pwdClaim == null) return new Result<bool>(false);
            var generateSaltResult = await _saltService.GenerateSalt();
            var hashSalt = generateSaltResult.Match<Guid>(salt => salt, e =>
            {
                _logger.LogError("Salt service failed while generating salt: {e}", e);
                return Guid.Empty;
            });

            var hash = GetHash(pwdClaim.Value, hashSalt.ToString());
            IUser newUser = new User()
            {
                Username = user.Username,
                Hash = hash,
                Salt = hashSalt
            };
            var createUserResult = await _repository.Create(newUser);

            return createUserResult.Match(succ =>
            {
                if (succ)
                {
                    return succ;
                }
                _logger.LogError("Something went wrong while processing create user repository method");
                return new Result<bool>(false);
            }, e =>
            {
                _logger.LogError("Create user repository method failed while processing: {e}", e);
                return new Result<bool>(e);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Register user service throw an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<bool>> Login(UserDTO user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var currentSaltResult = await _saltService.GetCurrentSalt();
            var currentSalt = currentSaltResult.Match<Guid>(salt => salt, e =>
            {
                _logger.LogError("Salt service failed while getting current salt: {e}", e);
                return Guid.Empty;
            });
            if (currentSalt == Guid.Empty) return new Result<bool>(new NullReferenceException("There is no current salt"));
            
            var key = currentSalt.ToByteArray();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            
            SecurityToken securityToken;
            _ = tokenHandler.ValidateToken(user.JWT, parameters, out securityToken);
            JwtSecurityToken jwtToken = (JwtSecurityToken)securityToken;

            var pwdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "password");
            
            var getUserResult = await _repository.Get(user.Username);
            var userToLogin = getUserResult.Match<User?>(usr => usr, e =>
            {
                _logger.LogError("Get user repository method failed while processing: {e}", e);
                return null;
            });
            if (userToLogin == null) return new Result<bool>(false);

            var hashToVerify = GetHash(pwdClaim.Value, userToLogin.Salt.ToString());
            var verifyLogin = VerifyHash(hashToVerify, userToLogin.Hash);
            return new Result<bool>(verifyLogin);
        }
        catch (Exception e)
        {
            _logger.LogError("Login user service throw an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    public async Task<Result<bool>> GetStatus(string username )
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError("Get user status service throw an exception: {e}", e);
            return new Result<bool>(e);
        }
    }

    private string GetHash(string pwd, string salt) => BCrypt.Net.BCrypt.HashPassword(pwd, salt);
    private bool VerifyHash(string pwd, string hash) => BCrypt.Net.BCrypt.Verify(pwd, hash);
}