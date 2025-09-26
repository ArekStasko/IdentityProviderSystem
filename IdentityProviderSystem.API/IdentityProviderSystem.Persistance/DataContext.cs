using IdentityProviderSystem.Domain.Models.Salt;
using IdentityProviderSystem.Domain.Models.Token;
using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance;

public class DataContext : DbContext, IUserDataContext, ISaltDataContext, IAccessTokenDataContext
{
    public DataContext(){}

    public DataContext(DbContextOptions<DataContext> options) : base(options){}

    public virtual DbSet<Salt> Salts { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public DbSet<AccessToken> AccessTokens { get; set; }
}