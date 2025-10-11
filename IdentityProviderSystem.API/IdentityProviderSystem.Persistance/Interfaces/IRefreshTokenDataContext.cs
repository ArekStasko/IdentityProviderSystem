using IdentityProviderSystem.Domain.Models.Token;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance.Interfaces;

public interface IRefreshTokenDataContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}