using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Initializer
{
    public static class Initializer
    {
        public static void InitializeDatabase(ShopDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
