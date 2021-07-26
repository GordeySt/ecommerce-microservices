using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.Common.Constatns;
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
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionConstants.NotFoundItemMessage);
            }

            var photoUploadResult = await _photoCloudAccessor.AddPhotoToCloudAsync(mainImage);

            return await _productRepository.UpdateMainImageAsync(product, photoUploadResult.Url);
        }
    }
}
