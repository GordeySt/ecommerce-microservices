using AutoMapper;
using Basket.API.BL.Interfaces;
using Basket.API.BL.Mappers;
using Basket.API.BL.Services;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.DAL.Interfaces.Redis;
using Basket.Tests.Shared.Services;
using FluentAssertions;
using Moq;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryStub = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceStub = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryStub = new();
        private readonly IMapper _mapper;

        public OrderServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task CheckoutShoppingCartAsync_WithUnexistingShoppingCart_ReturnsNotFoundServiceResult()
        {
            // Arrange
            var currentUserId = Guid.NewGuid();
            var order = OrderTestData.CreateOrderEntity();

            var orderService = new OrderService(_orderRepositoryStub.Object,
                _shoppingCartRepositoryStub.Object, _currentUserServiceStub.Object, _mapper);

            _shoppingCartRepositoryStub
                .Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync((ShoppingCart)null);

            // Act
            var result = await orderService.CheckoutShoppingCartAsync(order);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionConstants.NotFoundItemMessage);
        }

        [Fact]
        public async Task CheckoutShoppingCartAsync_WithExistingShoppingCart_ShouldUpdateOrderPropsFromShoppingCart()
        {
            // Arrange
            var currentUserId = Guid.NewGuid();
            var order = OrderTestData.CreateOrderEntity();
            var shoppingCart = ShoppingCartTestData.CreateShoppingCartEntity(currentUserId);

            var orderService = new OrderService(_orderRepositoryStub.Object,
                _shoppingCartRepositoryStub.Object, _currentUserServiceStub.Object, _mapper);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            _shoppingCartRepositoryStub
                .Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(shoppingCart);

            // Act
            var result = await orderService.CheckoutShoppingCartAsync(order);

            // Assert
            order.TotalPrice.Should().Be(shoppingCart.TotalPrice);
            order.ShoppingCartItems.Should().BeEquivalentTo(shoppingCart.ShoppingCartItems);
            order.UserId.Should().Be(shoppingCart.Id);
        }

        [Fact]
        public async Task CheckoutShoppingCartAsync_WithExistingShoppingCart_ReturnsSuccessfulServiceResult()
        {
            // Arrange
            var currentUserId = Guid.NewGuid();
            var order = OrderTestData.CreateOrderEntity();
            var shoppingCart = ShoppingCartTestData.CreateShoppingCartEntity(currentUserId);

            var orderService = new OrderService(_orderRepositoryStub.Object,
                _shoppingCartRepositoryStub.Object, _currentUserServiceStub.Object, _mapper);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            _shoppingCartRepositoryStub
                .Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(shoppingCart);

            // Act
            var result = await orderService.CheckoutShoppingCartAsync(order);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
        }

        [Fact]
        public async Task GetOrdersByUserIdAsync_WithExistingOrders_ReturnsOrders()
        {
            // Arrange
            var currentUserId = Guid.NewGuid();
            var order = OrderTestData.CreateOrderEntity();
            var pagingParams = CommonTestData.CreatePagingParams();
            var orders = OrderTestData.CreateOrdersWithPagingParams(pagingParams);

            var orderService = new OrderService(_orderRepositoryStub.Object,
                _shoppingCartRepositoryStub.Object, _currentUserServiceStub.Object, _mapper);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(currentUserId.ToString());

            _orderRepositoryStub
                .Setup(t => t.GetOrdersByUserIdAsync(It.IsAny<Guid>(), It.IsAny<PagingParams>()))
                .ReturnsAsync(orders);

            // Act
            var result = await orderService.GetOrdersByUserIdAsync(pagingParams);

            // Assert
            result.Count.Should().Be(pagingParams.PageSize);
        }
    }
}
