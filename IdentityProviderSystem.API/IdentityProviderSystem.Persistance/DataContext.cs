using IdentityProviderSystem.Domain.Models.User;
using IdentityProviderSystem.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityProviderSystem.Persistance;

public class DataContext : DbContext, IUserDataContext
{
    public DataContext(){}

    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    
    public virtual DbSet<User> Users { get; set; }
}