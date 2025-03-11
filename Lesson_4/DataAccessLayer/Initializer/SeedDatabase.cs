using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Initializer
{
    public class SeedDatabase
    {
        public void Seed(ShopDbContext context)
        {
            if(!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Price = 1500.00m },
                    new Product { Name = "Mouse", Price = 20.99m },
                    new Product { Name = "Keyboard", Price = 45.50m },
                    new Product { Name = "Monitor", Price = 300.00m }
                };
                
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
