using Microsoft.Extensions.DependencyInjection;
using SentinelBusinessLayer.Service;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Injections;
public static class BusinessLayerInjection
{
    public static void AddSentinelServices(this IServiceCollection services)
    {
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IHealthCareService, HealthCareService>();
        services.AddScoped<IVendorService, VendorService>();
    }
}