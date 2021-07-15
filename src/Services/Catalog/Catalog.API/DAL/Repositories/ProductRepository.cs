using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.API.PL.Models.Params;
using MongoDB.Driver;
using Services.Common.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class ProductRepository : AsyncBaseRepository<Product>,
        IProductRepository
    {
        public ProductRepository(IDatabaseContext databaseContext) : base(databaseContext)
        { }

        public async Task<PagedList<Product>> GetProductsByCategory(CategoryParams categoryParams)
        {
            var filter = Builders<Product>.Filter
                .Eq(p => p.Category, categoryParams.CategoryName);

            var collection = DatabaseContext.Products;

            return await PagedList<Product>.CreateAsync(collection, filter, categoryParams.PageNumber,
                categoryParams.PageSize);
        }

        public async Task UpdateMainImageAsync(Product product)
        {
            await DatabaseContext
                .Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
        }
    }
}
