using Basket.API.BL.Interfaces;
using Basket.API.BL.Services;
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

        [Fact]
        public async Task AddShoppingCartAsync_WithExistingProduct_ReturnsAddedItem()
        {
            // Arrange
            var shoppingCartToAdd = ShoppingCartTestData.CreateShoppingCartEntity();
            var currentUserId = Guid.NewGuid().ToString();

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object);

            _shoppingCartRepositoryStub
                .Setup(t => t.AddAsync(shoppingCartToAdd))
                .ReturnsAsync(shoppingCartToAdd);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId);

            // Act
            var result = await shoppingCartService.AddShoppingCartAsync(shoppingCartToAdd);

            // Assert
            result.Id.Should().Be(currentUserId);
            result.Should().BeEquivalentTo(shoppingCartToAdd);
        }

        [Fact]
        public async Task AddShoppingCartAsync_WithExistingProduct_ShouldUpdateTotalPriceCorrectly()
        {
            // Arrange
            var shoppingCartToAdd = ShoppingCartTestData.CreateShoppingCartEntity();
            var currentUserId = Guid.NewGuid().ToString();
            var correctTotalPrice = ShoppingCartTestData.CorrectTotalPrice;

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object,
                _currentUserServiceStub.Object);

            _shoppingCartRepositoryStub
                .Setup(t => t.AddAsync(shoppingCartToAdd))
                .ReturnsAsync(shoppingCartToAdd);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId);

            // Act
            var result = await shoppingCartService.AddShoppingCartAsync(shoppingCartToAdd);

            // Assert
            result.TotalPrice.Should().Be(correctTotalPrice);
        }
    }
}
