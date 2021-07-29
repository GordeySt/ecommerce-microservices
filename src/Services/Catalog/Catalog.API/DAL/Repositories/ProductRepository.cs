using Catalog.API.BL.Enums;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
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

        public void SortProductsByDefinition<T>(ref IQueryable<Product> products, OrderType? orderType,
            Expression<Func<Product, T>> sortDefinition)
        {
            switch (orderType)
            {
                case OrderType.Asc:
                    products = products.OrderBy(sortDefinition);
                    break;
                case OrderType.Desc:
                    products = products.OrderByDescending(sortDefinition);
                    break;
            }
        }
    }
}
