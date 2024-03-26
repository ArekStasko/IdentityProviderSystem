using IdentityProviderSystem.Domain.Models.User;
using LanguageExt.Common;

namespace IdentityProviderSystem.Persistance.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    public Task<Result<bool>> Create(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> Get(int id)
    {
        throw new NotImplementedException();
    }
}