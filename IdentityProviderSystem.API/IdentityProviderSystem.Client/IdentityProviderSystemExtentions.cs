using System;
using Microsoft.Extensions.DependencyInjection;
using IdentityProviderSystem.Client.Services;

namespace IdentityProviderSystem.Client
{

    public static class IdentityProviderSystemExtentions
    {

        public static IServiceCollection AddIdpHttpClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<ITokenService, TokenService>((services, client) =>
            {
                string baseUrl = "http://192.168.1.42:8080/api/idp-v1/";
                client.BaseAddress = new Uri(baseUrl);
            });

            return serviceCollection;
        }

    }

}
