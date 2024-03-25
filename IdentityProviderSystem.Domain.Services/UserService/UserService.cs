using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.UserService;

public class UserService : IUserService
{
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