using Catalog.API.BL.Constants;
using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoCloudAccessor _photoCloudAccessor;
        private readonly IProductRepository _productRepository;

        public PhotoService(IPhotoCloudAccessor photoCloudAccessor, 
            IProductRepository productRepository)
        {
            _photoCloudAccessor = photoCloudAccessor;
            _productRepository = productRepository;
        }

        public async Task<ServiceResult> AddPhotoAsync(IFormFile mainImage, Guid productId)
        {
            var photoUploadResult = await _photoCloudAccessor.AddPhotoToCloudAsync(mainImage);

            var product = await _productRepository.GetItemByIdAsync(productId);

            if (product is null)
            {
                return new ServiceResult(ServiceResultType.NotFound, 
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            product.MainImageUrl = photoUploadResult.Url;

            await _productRepository.UpdateMainImageAsync(product);

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
