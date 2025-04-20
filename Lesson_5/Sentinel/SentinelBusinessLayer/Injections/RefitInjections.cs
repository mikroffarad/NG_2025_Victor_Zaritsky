using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using SentinelAbstraction.Settings;
using SentinelBusinessLayer.Clients;
using System;

namespace SentinelBusinessLayer.Injections;

public static class RefitInjections
{
    public static void AddRefitClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PetStoreClientSettings>(
            configuration.GetSection("RefitClients").GetSection(PetStoreClientSettings.SectionName));
        services.Configure<HealthCareClientSettings>(
            configuration.GetSection("RefitClients").GetSection(HealthCareClientSettings.SectionName));

        services.AddRefitClient<IPetStoreClient>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<PetStoreClientSettings>>().Value;
                if (string.IsNullOrWhiteSpace(settings.BaseAddress))
                {
                    throw new InvalidOperationException($"BaseAddress for {PetStoreClientSettings.SectionName} is not configured in appsettings.");
                }
                client.BaseAddress = new Uri(settings.BaseAddress);
            });

        services.AddRefitClient<IHealthCareClient>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<HealthCareClientSettings>>().Value;
                if (string.IsNullOrWhiteSpace(settings.BaseAddress))
                {
                     throw new InvalidOperationException($"BaseAddress for {HealthCareClientSettings.SectionName} is not configured in appsettings.");
                }
                client.BaseAddress = new Uri(settings.BaseAddress);
            });
    }
}