using DAL_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStoreDal.Repositories;
using PetStoreDal.Repositories.Interfaces;

namespace PetStoreDal
{
    public static class PetStoreDalInjection
    {
        public static void AddPetStoreDalLogic(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PetStoreDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
        }
    }
}