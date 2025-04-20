using Microsoft.Extensions.DependencyInjection;
using PetStoreBL.Profiles;
using PetStoreBL.Services;
using PetStoreBL.Services.Interfaces;

namespace PetStoreBL
{
    public static class PetStoreBlInjection
    {
        public static void AddPetStoreBusinessLogic(
            this IServiceCollection services)
        {
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IStoreService, StoreService>();

            services.AddAutoMapper(typeof(PetStoreMappingProfile));
        }
    }
}