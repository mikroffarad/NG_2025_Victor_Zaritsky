using DAL_Core;
using HealthCareDal.Repositories;
using HealthCareDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCareDal
{
    public static class HealthCareDalInjection
    {
        public static void AddHealthCareDalLogic(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PetStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IHealthCareRepository, HealthCareRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
        }
    }
}