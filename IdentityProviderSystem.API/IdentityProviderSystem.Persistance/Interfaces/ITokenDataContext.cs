using IdentityProviderSystem.Domain.Models.Token;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance.Interfaces;

public interface ITokenDataContext
{
    public DbSet<AccessToken> Tokens { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}