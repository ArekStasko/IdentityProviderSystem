using IdentityProviderSystem.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance.Interfaces;

public interface IUserDataContext
{
    public DbSet<User> Users { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}