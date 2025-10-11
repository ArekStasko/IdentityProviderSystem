using IdentityProviderSystem.Domain.DTO;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Domain.Services.UserService;

public interface IUserService
{
    public Task<Result<SessionDTO>> Register(UserDTO user);
    public Task<Result<SessionDTO>> Login(UserDTO user);
    public Task<Result<SessionDTO>> RefreshSession(string refreshToken);
}