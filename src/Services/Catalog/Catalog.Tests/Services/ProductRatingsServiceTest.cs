using Catalog.API.BL.Constants;
using Catalog.API.BL.Interfaces;
using Catalog.API.BL.Services;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.Tests.Shared.Services;
using FluentAssertions;
using Moq;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Tests.Services
{
    public class ProductRatingsServiceTest
    {
        private readonly Mock<IProductRatingsRepository> _productRatingsRepositoryStub = new();
        private readonly Mock<IUsersRepository> _usersRepositoryStub = new();
        private readonly Mock<IProductRepository> _productRepositoryStub = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceStub = new();
        private readonly Random _rand = new();

        [Fact]
        public async Task AddRatingToProductAsync_WithUnexistingUser_ShouldReturnNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var expectedServiceResult = new ServiceResult(ServiceResultType.NotFound,
                ExceptionMessageConstants.NotFoundItemMessage);

            _usersRepositoryStub
                .Setup(t => t.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((User)null);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.AddRatingToProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionMessageConstants.NotFoundItemMessage);

            _currentUserServiceStub.Verify(x => x.UserId);
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task AddRatingToProductAsync_WithUnexistingProduct_ShouldReturnNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var expectedServiceResult = new ServiceResult(ServiceResultType.NotFound,
                ExceptionMessageConstants.NotFoundItemMessage);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((Product)null);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.AddRatingToProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionMessageConstants.NotFoundItemMessage);

            _currentUserServiceStub.Verify(x => x.UserId);
        }

        [Fact]
        public async Task AddRatingToProductAsync_WithExistingProductRating_ShouldReturnBadRequestServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var productRating = ProductRatingsTestData.CreateProductRating();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.BadRequest,
                ExceptionMessageConstants.AlreadyExistedRatinsMessage);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(productRating);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.AddRatingToProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(ExceptionMessageConstants.AlreadyExistedRatinsMessage);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task AddRatingToProductAsync_WithUnexistingProductRating_ShouldReturnSuccessfulServiceResultWithCreatedItem()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var productRating = ProductRatingsTestData.CreateProductRating();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.Success,
                productRating);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((ProductRating)null);

            _productRatingsRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ProductRating>()))
                .ReturnsAsync(expectedServiceResult);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.AddRatingToProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
            result.Data.Rating.Should().BeInRange(0, 5);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRatingsRepositoryStub.Verify(x => x.AddAsync(It.IsAny<ProductRating>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task AddRatingToProductAsync_WithUnexistingProductRating_ShouldUpdateTotalRating()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount1 = 5;
            var ratingCount2 = 2;
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var productRating = ProductRatingsTestData.CreateProductRating();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.Success,
                productRating);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((ProductRating)null);

            _productRatingsRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ProductRating>()))
                .ReturnsAsync(expectedServiceResult);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result1 = await productRatingsService.AddRatingToProductAsync(productId, ratingCount1);
            var result2 = await productRatingsService.AddRatingToProductAsync(productId, ratingCount2);

            // Assert
            productEntity.TotalRating.Should().Be(7);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRatingsRepositoryStub.Verify(x => x.AddAsync(It.IsAny<ProductRating>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task AddRatingToProductAsync_WithUnexistingProductRating_ShouldUpdateAverageRating()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount1 = 5;
            var ratingCount2 = 2;
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var productRating = ProductRatingsTestData.CreateProductRating();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.Success,
                productRating);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((ProductRating)null);

            _productRatingsRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ProductRating>()))
                .ReturnsAsync(expectedServiceResult);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result1 = await productRatingsService.AddRatingToProductAsync(productId, ratingCount1);
            var result2 = await productRatingsService.AddRatingToProductAsync(productId, ratingCount2);

            // Assert
            productEntity.AverageRating.Should().Be(3.5);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRatingsRepositoryStub.Verify(x => x.AddAsync(It.IsAny<ProductRating>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task UpdateRatingAtProductAsync_WithUnexistingUser_ShouldReturnNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var expectedServiceResult = new ServiceResult(ServiceResultType.NotFound,
                ExceptionMessageConstants.NotFoundItemMessage);

            _usersRepositoryStub
                .Setup(t => t.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((User)null);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionMessageConstants.NotFoundItemMessage);

            _currentUserServiceStub.Verify(x => x.UserId);
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task UpdateRatingAtProductAsync_WithUnexistingProduct_ShouldReturnNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var expectedServiceResult = new ServiceResult(ServiceResultType.NotFound,
                ExceptionMessageConstants.NotFoundItemMessage);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((Product)null);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionMessageConstants.NotFoundItemMessage);

            _currentUserServiceStub.Verify(x => x.UserId);
        }

        [Fact]
        public async Task UpdateRatingAtProductAsync_WithUnexistingProductRating_ShouldReturnNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount = _rand.Next(5);
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.NotFound,
                ExceptionMessageConstants.NotFoundItemMessage);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((ProductRating)null);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionMessageConstants.NotFoundItemMessage);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task UpdateRatingAtProduct_WithExistingProductRating_ShouldUpdateTotalRating()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount1 = 1;
            var ratingCount2 = 2;
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var productRating = ProductRatingsTestData.CreateProductRating();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.Success,
                productRating);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(productRating);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            _productRatingsRepositoryStub
                .Setup(x => x.UpdateAsync(It.IsAny<ProductRating>()))
                .ReturnsAsync(new ServiceResult(ServiceResultType.Success));

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result1 = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount1);
            var result2 = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount2);

            // Assert
            productEntity.TotalRating.Should().Be(-3);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task UpdateRatingAtProductAsync_WithExistingProductRating_ShouldUpdateAverageRating()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var ratingCount1 = 1;
            var ratingCount2 = 4;
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var productRating = ProductRatingsTestData.CreateProductRating();
            var expectedServiceResult = new ServiceResult<ProductRating>(ServiceResultType.Success,
                productRating);

            _productRatingsRepositoryStub
                .Setup(t => t.GetProductRatingByIdsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(productRating);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(userId);

            _usersRepositoryStub
                .Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(userEntity);

            _productRepositoryStub
                .Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productEntity);

            _productRatingsRepositoryStub
                .Setup(x => x.UpdateAsync(It.IsAny<ProductRating>()))
                .ReturnsAsync(new ServiceResult(ServiceResultType.Success));

            var productRatingsService = new ProductRatingsService(_usersRepositoryStub.Object,
                _productRepositoryStub.Object, _productRatingsRepositoryStub.Object,
                _currentUserServiceStub.Object);

            // Act
            var result1 = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount1);
            var result2 = await productRatingsService.UpdateRatingAtProductAsync(productId, ratingCount2);

            // Assert
            productEntity.AverageRating.Should().Be(-1);

            _currentUserServiceStub.Verify(x => x.UserId);
            _productRatingsRepositoryStub.Verify(x => x.GetProductRatingByIdsAsync(It.IsAny<Guid>(),
                It.IsAny<Guid>()));
            _productRepositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _usersRepositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), true));
        }
    }
}
