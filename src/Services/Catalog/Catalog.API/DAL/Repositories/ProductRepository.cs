using Catalog.API.BL.Enums;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class ProductRepository : AsyncBaseRepository<Product>,
        IProductRepository
    {
        public ProductRepository(ApplicationDbContext databaseContext) : base(databaseContext)
        { }

        public async Task<Product> GetProductByIdAsync(Guid id, bool disableTracking = true)
        {
            if (disableTracking)
            {
                return await DatabaseContext.Products
                    .Include(t => t.Ratings)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }

            return await DatabaseContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

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

        public async Task<List<string>> GetPopularCategoriesAsync(int popularCategoriesCount)
        {
            var products = GetAllQueryable();

            return await products
                .GroupBy(x => x.Category)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .Take(popularCategoriesCount)
                .ToListAsync();
        }

        public void SortProductsByDefinition<T>(ref IQueryable<Product> products, OrderType? orderType,
            Expression<Func<Product, T>> sortDefinition)
        {
            products = orderType switch
            {
                OrderType.Asc => products.OrderBy(sortDefinition),
                OrderType.Desc => products.OrderByDescending(sortDefinition),
                _ => products
            };
        }
    }
}
