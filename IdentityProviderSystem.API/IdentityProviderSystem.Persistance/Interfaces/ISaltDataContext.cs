using IdentityProviderSystem.Domain.Models.Salt;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance.Interfaces;

public interface ISaltDataContext
{
    public DbSet<Salt> Salts { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}