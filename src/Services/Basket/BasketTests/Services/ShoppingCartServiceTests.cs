using AutoMapper;
using Basket.API.BL.Interfaces;
using Basket.API.BL.Mappers;
using Basket.API.BL.Services;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using Basket.Tests.Shared.Services;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Tests.Services
{
    public class ShoppingCartServiceTests
    {
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryStub = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceStub = new();
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public ShoppingCartServiceTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShoppingCartProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task AddShoppingCartAsync_WithExistingProduct_ReturnsAddedItem()
        {
            // Arrange
            var shoppingCartToAddDto = ShoppingCartTestData.CreateAddShoppingCartDto();
            var currentUserId = Guid.NewGuid().ToString();
            var shoppingCartEntity = ShoppingCartTestData.CreateShoppingCartEntity(new Guid(currentUserId));

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(shoppingCartEntity);
                
            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId);

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
            var currentUserId = Guid.NewGuid().ToString();
            var shoppingCartEntity = ShoppingCartTestData.CreateShoppingCartEntity(new Guid(currentUserId));
            var correctTotalPrice = ShoppingCartTestData.CorrectTotalPrice;

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.AddAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(shoppingCartEntity);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId);

            // Act
            var result = await shoppingCartService.AddShoppingCartAsync(shoppingCartToAddDto);

            // Assert
            result.TotalPrice.Should().Be(correctTotalPrice);
        }
    }
}
