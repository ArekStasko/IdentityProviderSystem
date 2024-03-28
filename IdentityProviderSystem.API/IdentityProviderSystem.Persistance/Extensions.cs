using IdentityProviderSystem.Persistance.Interfaces;
using IdentityProviderSystem.Persistance.Repositories.SaltRepository;
using IdentityProviderSystem.Persistance.Repositories.UserRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProviderSystem.Persistance;

public static class Extensions
{
    public static void AddDataContext(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<DataContext>(options =>
        {
            options
                .UseSqlServer(connectionString);
        });
    }
    
    public static void MigrateReadDatabase(this IApplicationBuilder app) => DataMigrationService.MigrationInitialization(app);
    
    public static void AddRepositories(this IServiceCollection serivces)
    {
        serivces.AddScoped<IUserRepository, UserRepository>();
        serivces.AddScoped<ISaltRepository, SaltRepository>();
    }
    
    private static string GetConnectionString()
    {
        var databaseServer = Environment.GetEnvironmentVariable("DatabaseServer");
        var databasePort = Environment.GetEnvironmentVariable("DatabasePort");
        var databaseUser = Environment.GetEnvironmentVariable("DatabaseUser");
        var databasePassword = Environment.GetEnvironmentVariable("DatabasePassword");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var connectionString =
            $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
        return connectionString;
    }
}