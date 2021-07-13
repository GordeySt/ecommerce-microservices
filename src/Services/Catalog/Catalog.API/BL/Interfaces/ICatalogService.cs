using Catalog.API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IReadOnlyCollection<Product>> GetProductsByCategory(string categoryName);
        Task AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
