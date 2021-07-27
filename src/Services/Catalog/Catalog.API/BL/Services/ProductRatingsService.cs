using Catalog.API.BL.Constants;
using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services
{
    public class ProductRatingsService : IProductRatingsService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductRatingsRepository _productRatingsRepository;
        private readonly ICurrentUserService _currentUserSerivce;

        public ProductRatingsService(IUsersRepository usersRepository, 
            IProductRepository productRepository, 
            IProductRatingsRepository productRatingsRepository, 
            ICurrentUserService currentUserSerivce)
        {
            _usersRepository = usersRepository;
            _productRepository = productRepository;
            _productRatingsRepository = productRatingsRepository;
            _currentUserSerivce = currentUserSerivce;
        }

        public async Task<ServiceResult<ProductRating>> AddRatingToProductAsync(Guid productId, int ratingCount)
        {
            var userId = new Guid(_currentUserSerivce.UserId);

            var user = await _usersRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return new ServiceResult<ProductRating>(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product is null)
            {
                return new ServiceResult<ProductRating>(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var productRating = await _productRatingsRepository.GetProductRatingByIdsAsync(productId, userId);

            if (productRating is not null)
            {
                return new ServiceResult<ProductRating>(ServiceResultType.BadRequest,
                    ExceptionMessageConstants.AlreadyExistedRatinsMessage);
            }

            var newProductRating = CreateNewProductRating(user, product, ratingCount);

            product.TotalRating += ratingCount;

            var result = await _productRatingsRepository.AddAsync(newProductRating);

            return result;
        }

        private static ProductRating CreateNewProductRating(User user, Product product, int ratingCount) => new()
        {
            User = user,
            Product = product,
            Rating = ratingCount
        };

        public async Task<ServiceResult> UpdateRatingAtProductAsync(Guid productId, int ratingCount)
        {
            var userId = new Guid(_currentUserSerivce.UserId);

            var user = await _usersRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var productRating = await _productRatingsRepository.GetProductRatingByIdsAsync(productId, userId);

            if (productRating is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var lastProductRating = productRating.Rating;

            UpdateProductRating(productRating, ratingCount);

            var updateRatingResult = await _productRatingsRepository.UpdateAsync(productRating);

            if (updateRatingResult.Result is ServiceResultType.Success)
            {
                UpdateProductTotalRating(product, lastProductRating, ratingCount);
            }

            var updateTotalRatingResult = await _productRepository.UpdateAsync(product);

            return updateTotalRatingResult;
        }

        private static void UpdateProductRating(ProductRating productRating, int ratingCount)
        {
            productRating.Rating = ratingCount;
        }

        private static void UpdateProductTotalRating(Product product, int lastProductRating, int ratingCount)
        {
             product.TotalRating = product.TotalRating - lastProductRating + ratingCount;
        }
    }
}
