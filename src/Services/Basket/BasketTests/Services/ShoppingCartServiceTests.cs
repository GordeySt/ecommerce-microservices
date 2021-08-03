using Basket.API.BL.Services;
using Basket.API.DAL.Interfaces.Redis;
using Basket.Tests.Shared.Services;
using FluentAssertions;
using Moq;
using Services.Common.Enums;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Tests.Services
{
    public class ShoppingCartServiceTests
    {
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryStub = new();
        [Fact]
        public async Task AddShoppingCartAsync_WithExistingProduct_ReturnsSuccessfulServiceResultWithAddedItem()
        {
            // Arrange
            var shoppingCartToAdd = ShoppingCartTestData.CreateShoppingCartEntity();

            var shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryStub.Object);

            // Act
            var result = await shoppingCartService.AddShoppingCartAsync(shoppingCartToAdd);

            // Assert
            result.Should().BeEquivalentTo(shoppingCartToAdd);
        }
    }
}
