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
        public Task<ServiceResult> UpdateMainImageAsync(Product product, string photoUrl);
        public Task<IEnumerable<string>> GetPopularCategoriesAsync(int popularCategoriesCount);
        public void SortProductsByDefinition<T>(ref IQueryable<Product> products, OrderType? orderType,
            Expression<Func<Product, T>> sortDefinition);
        public Task<Product> GetProductByIdAsync(Guid id, bool disableTracking = true);
    }
}
