using IdentityProviderSystem.Domain.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProviderSystem.Domain.Services;

public static class Extensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService.UserService>();
    }

    public static void AddServicesMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}