using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsByPrice(decimal MinPrice, decimal MaxPrice)
        {
            return await _dbSet
                .Where(p => p.Price >= MinPrice && p.Price <= MaxPrice)
                .ToListAsync();
        }
    }
}
