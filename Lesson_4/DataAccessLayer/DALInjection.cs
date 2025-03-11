using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Initializer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DALInjection
    {
        public static void AddDataAccessLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();;
            services.AddScoped<SeedDatabase>();
        }
    }
}
