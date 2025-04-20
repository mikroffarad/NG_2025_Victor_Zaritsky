using Microsoft.Extensions.DependencyInjection;
using HealthCareBL.Profiles;
using HealthCareBL.Services;
using HealthCareBL.Services.Interfaces;

namespace HealthCareBL
{
    public static class HealthCareBlInjection
    {
        public static void AddHealthCareBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IHealthCareService, HealthCareService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddAutoMapper(typeof(HealthCareMappingProfile));
        }
    }
}