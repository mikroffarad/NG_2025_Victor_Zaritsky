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
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Name = "Computer Peripherals", Description = "Accessories for computers" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var electronicsCategory = context.Categories.FirstOrDefault(c => c.Name == "Electronics");
                var peripheralsCategory = context.Categories.FirstOrDefault(c => c.Name == "Computer Peripherals");

                var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 1500.00m, CategoryId = electronicsCategory.Id },
                new Product { Name = "Mouse", Price = 20.99m, CategoryId = peripheralsCategory.Id },
                new Product { Name = "Keyboard", Price = 45.50m, CategoryId = peripheralsCategory.Id },
                new Product { Name = "Monitor", Price = 300.00m, CategoryId = electronicsCategory.Id }
            };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
