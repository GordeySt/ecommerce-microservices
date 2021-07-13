using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class ProductRepository : AsyncBaseRepository<Product>,
        IProductRepository
    {
        public ProductRepository(IDatabaseContext databaseContext) : base(databaseContext)
        { }

        public async Task<IReadOnlyCollection<Product>> GetProductsByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter
                .Eq(p => p.Category, categoryName);

            return await databaseContext
                .Products
                .Find(filter)
                .ToListAsync();
        }
    }
}
