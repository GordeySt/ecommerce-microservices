using Catalog.API.BL.Enums;
using Catalog.API.DAL.Entities;
using Services.Common.ResultWrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRepository : IAsyncBaseRepository<Product>
    {
        public Task<ServiceResult> UpdateMainImageAsync(Product product, string photoUrl);
        public Task<IEnumerable<string>> GetPopularCategoriesAsync(int popularCategoriesCount);
        public IQueryable<Product> SortProductsByPrice(ref IQueryable<Product> products, PriceOrderType? priceOrderType);
    }
}
