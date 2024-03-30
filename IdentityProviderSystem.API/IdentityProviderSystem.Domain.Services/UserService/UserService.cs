using AutoMapper;
using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Persistance.Repositories.UserRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Domain.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<IUserService> _logger;
    
    public UserService(IUserRepository repository, IMapper mapper, ILogger<IUserService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Register(UserDTO user)
    {
        try
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
}