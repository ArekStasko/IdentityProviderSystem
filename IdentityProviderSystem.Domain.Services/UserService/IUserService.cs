using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.UserService;

public interface IUserService
{
    public Task<Result<bool>> Register(UserDTO user);
    public Task<Result<bool>> Login(UserDTO user);
    public Task<Result<bool>> GetStatus(int id);
}