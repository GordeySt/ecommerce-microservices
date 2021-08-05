using Catalog.API.BL.Interfaces;
using Catalog.API.BL.Services;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.UnitTests.Shared.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTests.Services
{
    public class PhotoServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryStub = new();
        private readonly Mock<IPhotoCloudAccessor> _photoCloudAccessorStub = new();

        [Fact]
        public async Task AddPhotoAsync_WithUnexistingProduct_ReturnsNotFoundServiceResult()
        {
            // Arrange 
            var productId = Guid.NewGuid();
            var mainImage = PhotoServiceTestData.CreateFakeFormFile();

            _productRepositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((Product)null);

            var photoService = new PhotoService(_photoCloudAccessorStub.Object,
                _productRepositoryStub.Object);

            // Act
            var result = await photoService.AddPhotoAsync(mainImage, productId);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionConstants.NotFoundItemMessage);

            _productRepositoryStub.Verify(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task AddPhotoAsync_WithExistingProduct_ReturnsNotFoundServiceResult()
        {
            // Arrange 
            var productId = Guid.NewGuid();
            var product = CatalogServiceTestData.CreateProductEntity();
            var photoUploadResult = PhotoServiceTestData.CreatePhotoUploadResult();
            var mainImage = PhotoServiceTestData.CreateFakeFormFile();
            var expectedServiceResult = new ServiceResult(ServiceResultType.Success);

            _productRepositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(product);

            _productRepositoryStub
                .Setup(t => t.UpdateMainImageAsync(It.IsAny<Product>(), It.IsAny<string>()))
                .ReturnsAsync(expectedServiceResult);

            _photoCloudAccessorStub
                .Setup(t => t.AddPhotoToCloudAsync(It.IsAny<IFormFile>()))
                .ReturnsAsync(photoUploadResult);

            var photoService = new PhotoService(_photoCloudAccessorStub.Object,
                _productRepositoryStub.Object);

            // Act
            var result = await photoService.AddPhotoAsync(mainImage, productId);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            _productRepositoryStub.Verify(t => t.GetProductByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()));
            _productRepositoryStub.Verify(t => t.UpdateMainImageAsync(It.IsAny<Product>(), It.IsAny<string>()));
            _photoCloudAccessorStub.Verify(t => t.AddPhotoToCloudAsync(It.IsAny<IFormFile>()));
        }
    }
}
