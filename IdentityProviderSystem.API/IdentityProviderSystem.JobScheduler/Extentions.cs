using Coravel;
using IdentityProviderSystem.JobScheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProviderSystem.JobScheduler;

public static class Extentions
{
    public static void AddJobs(this IServiceCollection services)
    {
        services.AddScheduler();
        services.AddTransient<SaltJob>();
        services.AddTransient<AccessTokenJob>();
        services.AddTransient<RefreshTokenJob>();
    }
}