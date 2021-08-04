using AutoMapper;
using Basket.API.BL.Interfaces;
using Basket.API.BL.Mappers;
using Basket.API.BL.Services;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using Basket.Tests.Shared.Services;
using FluentAssertions;
using Moq;
using Services.Common.Constatns;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Tests.Services
{
    public class ShoppingCartServiceTests
    {
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryStub = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceStub = new();
        private readonly IMapper _mapper;

        public ShoppingCartServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShoppingCartProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task AddShoppingCartAsync_WithExistingProduct_ReturnsAddedItem()
        {
            // Arrange
            var shoppingCartToAddDto = ShoppingCartTestData.CreateAddShoppingCartDto();
            var currentUserId = Guid.NewGuid();
            var shoppingCartEntity = ShoppingCartTestData.CreateShoppingCartEntity(currentUserId);

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(shoppingCartEntity);
                
            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            // Act
            var result = await shoppingCartService.AddShoppingCartAsync(shoppingCartToAddDto);

            // Assert
            result.Id.Should().Be(currentUserId);
            result.Should().BeEquivalentTo(
                shoppingCartToAddDto,
                options => options.ComparingByMembers<ShoppingCart>());
        }

        [Fact]
        public async Task AddShoppingCartAsync_WithExistingProduct_ShouldUpdateTotalPriceCorrectly()
        {
            // Arrange
            var shoppingCartToAddDto = ShoppingCartTestData.CreateAddShoppingCartDto();
            var currentUserId = Guid.NewGuid();
            var shoppingCartEntity = ShoppingCartTestData.CreateShoppingCartEntity(currentUserId);
            var correctTotalPrice = ShoppingCartTestData.CorrectTotalPrice;

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(shoppingCartEntity);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            // Act
            var result = await shoppingCartService.AddShoppingCartAsync(shoppingCartToAddDto);

            // Assert
            result.TotalPrice.Should().Be(correctTotalPrice);
        }

        [Fact]
        public async Task GetShoppingCartByIdAsync_WithUnexistingItem_ReturnsNotFoundServiceResult()
        {
            // Arrange
            var currentUserId = Guid.NewGuid();

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync((ShoppingCart)null);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            // Act
            var result = await shoppingCartService.GetShoppingCartByIdAsync();

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionConstants.NotFoundItemMessage);
        }

        [Fact]
        public async Task GetShoppingCartByIdAsync_WithExistingItem_ReturnsSuccessfulServiceResultWithShoppingCart()
        {
            // Arrange
            var currentUserId = Guid.NewGuid();
            var shoppingCartEntity = ShoppingCartTestData.CreateShoppingCartEntity(currentUserId);
            var shoppingCartDto = ShoppingCartTestData.CreateShoppingCartDto(currentUserId);

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(shoppingCartEntity);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            // Act
            var result = await shoppingCartService.GetShoppingCartByIdAsync();

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
            result.Data.Should().BeEquivalentTo(
                shoppingCartDto,
                options => options.ComparingByMembers<ShoppingCart>());
        }
    }
}
