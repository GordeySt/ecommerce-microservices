using Catalog.API.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRatingsRepository : IAsyncBaseRepository<ProductRating>
    {
        public Task<ProductRating> GetProductRatingByIdsAsync(Guid productId, Guid userId);
    }
}
