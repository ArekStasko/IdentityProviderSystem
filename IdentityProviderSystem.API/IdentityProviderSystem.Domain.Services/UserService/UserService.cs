using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Persistance.Repositories.UserRepository;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;

namespace IdentityProviderSystem.Domain.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<IUserService> _logger;
    
    public UserService(IUserRepository repository, ILogger<IUserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Register(UserDTO user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> Login(UserDTO user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> GetStatus(int id)
    {
        throw new NotImplementedException();
    }
}