using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.UserRepository;

public interface IUserRepository
{
    public Task<Result<int>> Create(IUser user);
    public Task<Result<bool>> Update(IUser user);
    public Task<Result<User>> Get(string username);
}