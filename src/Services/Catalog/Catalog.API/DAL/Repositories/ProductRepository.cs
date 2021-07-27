using Catalog.API.BL.Enums;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class ProductRepository : AsyncBaseRepository<Product>,
        IProductRepository
    {
        public ProductRepository(ApplicationDbContext databaseContext) : base(databaseContext)
        { }

        public async Task<ServiceResult> UpdateMainImageAsync(Product product, string photoUrl)
        {
            product.MainImageUrl = photoUrl;

            var success = await DatabaseContext.SaveChangesAsync() > 0;

            if (!success)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    ExceptionConstants.ProblemCreatingItemMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<IEnumerable<string>> GetPopularCategoriesAsync(int popularCategoriesCount)
        {
            var products = GetAllQueryable();

            return await products.GroupBy(x => x.Category)
                .Select(x => new { Category = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Category)
                .Take(popularCategoriesCount)
                .ToListAsync();
        }

        public IQueryable<Product> SortProductsByPrice(ref IQueryable<Product> products, PriceOrderType? priceOrderType)
        {
            switch (priceOrderType)
            {
                case PriceOrderType.Asc:
                    products = products.OrderBy(t => t.Price);
                    break;
                case PriceOrderType.Desc:
                    products = products.OrderByDescending(t => t.Price);
                    break;
            }

            return products;
        }
    }
}
