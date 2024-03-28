using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.UserRepository;

public interface IUserRepository
{
    public Task<Result<bool>> Create(User user);
    public Task<Result<bool>> Update(User user);
    public Task<Result<User>> Get(string username);
}