﻿using AutoMapper;
using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Domain.Services.SaltService;
using IdentityProviderSystem.Domain.Services.TokenService;
using IdentityProviderSystem.Persistance.Repositories.UserRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Domain.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ISaltService _saltService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<IUserService> _logger;
    
    public UserService(IUserRepository repository, ISaltService saltService, ITokenService tokenService, IMapper mapper, ILogger<IUserService> logger)
    {
        _repository = repository;
        _saltService = saltService;
        _tokenService = tokenService;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Result<ITokenResponse>> Register(UserDTO user)
    {
        try
        {
            var generateSaltResult = await _saltService.GenerateSalt();
            var hashSalt = generateSaltResult.Match<string>(salt => salt, e =>
            {
                _logger.LogError("Salt service failed while generating salt: {e}", e);
                return "";
            });

            var hash = GetHash(user.Password, hashSalt.ToString());
            IUser newUser = new User()
            {
                Username = user.Username,
                Hash = hash,
                Salt = hashSalt
            };
            var createUserResult = await _repository.Create(newUser);

            int userId = createUserResult.Match(succ => succ, e =>
            {
                _logger.LogError("Create user repository method failed while processing: {e}", e);
                throw e;
            });

            var token = await _tokenService.Generate(userId);
            return token.Match(succ => new Result<ITokenResponse>((ITokenResponse)succ), err =>
            {
                _logger.LogError("Token service failed while generating token: {e}", err);
                return new Result<ITokenResponse>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Register user service throw an exception: {e}", e);
            return new Result<ITokenResponse>(e);
        }
    }

    public async Task<Result<ITokenResponse>> Login(UserDTO user)
    {
        try
        {
            var getUserResult = await _repository.Get(user.Username);
            var userToLogin = getUserResult.Match<User?>(usr => usr, e =>
            {
                _logger.LogError("Get user repository method failed while processing: {e}", e);
                return null;
            });
            if (userToLogin == null) return new Result<ITokenResponse>(new NullReferenceException());

            var verifyLogin = VerifyHash(user.Password, userToLogin.Hash);
            if (verifyLogin)
            {
                var token = await _tokenService.Generate(userToLogin.Id);
                return token.Match(succ => new Result<ITokenResponse>((ITokenResponse)succ), err =>
                {
                    _logger.LogError("Token service failed while generating token: {e}", err);
                    return new Result<ITokenResponse>(err);
                });
            }

            return new Result<ITokenResponse>(new InvalidOperationException());
        }
        catch (Exception e)
        {
            _logger.LogError("Login user service throw an exception: {e}", e);
            return new Result<ITokenResponse>(e);
        }
    }

    private string GetHash(string pwd, string salt) => BCrypt.Net.BCrypt.HashPassword(pwd, salt);
    private bool VerifyHash(string pwd, string hash) => BCrypt.Net.BCrypt.Verify(pwd, hash);
}