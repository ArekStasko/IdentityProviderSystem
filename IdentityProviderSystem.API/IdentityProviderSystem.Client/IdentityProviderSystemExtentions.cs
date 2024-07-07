﻿using System;
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
                string baseUrl = "";
                client.BaseAddress = new Uri(baseUrl);
            });

            return serviceCollection;
        }

    }

}
