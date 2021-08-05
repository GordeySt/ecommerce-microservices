using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.BL.Services;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.UnitTests.Shared.Services;
using FluentAssertions;
using Moq;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTests.Services
{
    public class CatalogServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryStub = new();
        private readonly Random _rand = new();
        private readonly IMapper _mapper;

        public CatalogServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task AddProductAsync_WithExistingProduct_ReturnsSuccessfulResultWithCreatedProduct()
        {
            // Arrange
            var productToCreate = CatalogServiceTestData.CreateCreateProductDto();
            var productEntity = CatalogServiceTestData.CreateProductEntity();
            var expectedServiceResult = new ServiceResult<Product>(ServiceResultType.Success, 
                productEntity);

            _repositoryStub
                .Setup(t => t.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(expectedServiceResult);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var creationResult = await catalogService.AddProductAsync(productToCreate);

            // Assert
            creationResult.Data.Id.Should().NotBeEmpty();
            creationResult.Data.Should().BeEquivalentTo(
                productToCreate,
                options => options.ComparingByMembers<Product>());
            creationResult.Result.Should().Be(ServiceResultType.Success);

            _repositoryStub.Verify(x => x.AddAsync(It.IsAny<Product>()));
        }

        [Fact]
        public async Task DeleteProductAsync_WithUnexistingItem_ReturnsNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _repositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((Product)null);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var deleteResult = await catalogService.DeleteProductAsync(productId);

            // Assert
            deleteResult.Result.Should().Be(ServiceResultType.NotFound);
            deleteResult.Message.Should().Be(ExceptionConstants.NotFoundItemMessage);

            _repositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task DeleteProductAsync_WithExistingItem_ReturnsNotFoundServiceResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productToDelete = CatalogServiceTestData.CreateProductEntity();
            var expectedServiceResult = new ServiceResult(ServiceResultType.Success);

            _repositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productToDelete);

            _repositoryStub
                .Setup(t => t.DeleteAsync(It.IsAny<Product>()))
                .ReturnsAsync(expectedServiceResult);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var deleteResult = await catalogService.DeleteProductAsync(productId);

            // Assert
            deleteResult.Result.Should().Be(ServiceResultType.Success);

            _repositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
            _repositoryStub.Verify(x => x.DeleteAsync(It.IsAny<Product>()));
        }

        [Fact]
        public async Task GetPopularCategoriesAsync_WithExistingCategories_ReturnsGivenAmountOfPopularCategories()
        {
            // Arrange
            var popularCategoriesCount = _rand.Next(5);
            var popularCategories = CatalogServiceTestData.GetPopularCategories(popularCategoriesCount);

            _repositoryStub
                .Setup(t => t.GetPopularCategoriesAsync(popularCategoriesCount))
                .ReturnsAsync(popularCategories);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var categories = await catalogService.GetPopularCategoriesAsync(popularCategoriesCount);

            // Assert
            categories.Should().HaveCount(popularCategoriesCount);

            _repositoryStub.Verify(x => x.GetPopularCategoriesAsync(popularCategoriesCount));
        }

        [Fact]
        public async Task GetProductByIdAsync_WithUnexistingProduct_ReturnsNull()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _repositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), false))
                .ReturnsAsync((Product)null);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var product = await catalogService.GetProductByIdAsync(productId);

            // Assert
            product.Should().BeNull();

            _repositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), false));
        }

        [Fact]
        public async Task GetProductByIdAsync_WithUnexistingProduct_ReturnsExpectedProductDto()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = CatalogServiceTestData.CreateProductEntity();
            var expectedProductDto = CatalogServiceTestData.CreateProductDto();

            _repositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), false))
                .ReturnsAsync(expectedProduct);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var product = await catalogService.GetProductByIdAsync(productId);

            // Assert
            product.Should().BeEquivalentTo(expectedProductDto);

            _repositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), false));
        }

        [Fact]
        public async Task UpdateProductAsync_WithUnexistingProduct_ReturnsNotFoundServiceResult()
        {
            // Arrange
            var updateProductDto = CatalogServiceTestData.CreateUpdateProductDto();

            _repositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync((Product)null);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var updateResult = await catalogService.UpdateProductAsync(updateProductDto);

            // Assert
            updateResult.Result.Should().Be(ServiceResultType.NotFound);
            updateResult.Message.Should().Be(ExceptionConstants.NotFoundItemMessage);

            _repositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
        }

        [Fact]
        public async Task UpdateProductAsync_WithExistingProduct_ReturnsSuccessfulServiceResult()
        {
            // Arrange
            var updateProductDto = CatalogServiceTestData.CreateUpdateProductDto();
            var productToUpdate = CatalogServiceTestData.CreateProductEntity();
            var expectedServiceResult = new ServiceResult(ServiceResultType.Success);

            _repositoryStub
                .Setup(t => t.GetProductByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(productToUpdate);

            _repositoryStub
                .Setup(t => t.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(expectedServiceResult);

            var catalogService = new CatalogService(_repositoryStub.Object, _mapper);

            // Act
            var updateResult = await catalogService.UpdateProductAsync(updateProductDto);

            // Assert
            updateResult.Result.Should().Be(ServiceResultType.Success);

            _repositoryStub.Verify(x => x.UpdateAsync(It.IsAny<Product>()));
            _repositoryStub.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>(), true));
        }
    }
}
