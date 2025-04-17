using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductByPriceAsync(decimal MinPrice, decimal MaxPrice);
        Task AddProductAsync (Product product);
        Task UpdateProductAsync (Product product);
        Task DeleteProductAsync (int id);
    }
}
