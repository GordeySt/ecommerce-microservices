using Catalog.API.DAL.Entities;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface IProductRatingsService
    {
        public Task<ServiceResult<ProductRating>> AddRatingToProductAsync(Guid productId, int ratingCount);
    }
}
