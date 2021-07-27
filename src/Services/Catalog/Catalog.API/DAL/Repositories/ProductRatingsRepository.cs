using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class ProductRatingsRepository : AsyncBaseRepository<ProductRating>,
        IProductRatingsRepository
    {

        public ProductRatingsRepository(ApplicationDbContext databaseContext) : base(databaseContext)
        { }

        public async Task<ProductRating> GetProductRatingByIdsAsync(Guid productId, Guid userId) => 
            await DatabaseContext.Ratings
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
    }
}
