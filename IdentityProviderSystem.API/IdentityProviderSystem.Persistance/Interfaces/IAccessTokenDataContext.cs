using IdentityProviderSystem.Domain.Models.Token;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance.Interfaces;

public interface IAccessTokenDataContext
{
    public DbSet<AccessToken> AccessTokens { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}