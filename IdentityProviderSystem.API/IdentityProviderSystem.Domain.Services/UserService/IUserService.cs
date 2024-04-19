using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.UserService;

public interface IUserService
{
    public Task<Result<ITokenResponse>> Register(UserDTO user);
    public Task<Result<ITokenResponse>> Login(UserDTO user);
    public Task<Result<bool>> GetStatus(string username);
}