using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.Params;
using Services.Common.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRepository : IAsyncBaseRepository<Product>
    {
        Task<PagedList<Product>> GetProductsByCategory(CategoryParams categoryParams);
        Task UpdateMainImageAsync(Product product);
    }
}
