using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);
        Task AddProductAsync(Product product);
        Task<ServiceResult> UpdateProductAsync(Product product);
        Task<ServiceResult> DeleteProductAsync(Guid id);
    }
}
