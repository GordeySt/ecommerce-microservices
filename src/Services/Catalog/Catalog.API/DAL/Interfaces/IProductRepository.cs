using Catalog.API.BL.Enums;
using Catalog.API.DAL.Entities;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRepository : IAsyncBaseRepository<Product>
    {
        Task<ServiceResult> UpdateMainImageAsync(Product product, string photoUrl);
        Task<List<string>> GetPopularCategoriesAsync(int popularCategoriesCount);
        void SortProductsByDefinition<T>(ref IQueryable<Product> products, OrderType? orderType,
            Expression<Func<Product, T>> sortDefinition);
        Task<Product> GetProductByIdAsync(Guid id, bool disableTracking = true);
    }
}
