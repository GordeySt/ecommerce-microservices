using AutoMapper;
using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.DAL.Interfaces.Redis;
using Basket.API.PL.Models.DTOs;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository, ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CheckoutShoppingCartAsync(Order order)
        {
            var currentUserId = _currentUserService.UserId;

            var shoppingCart = await _shoppingCartRepository.GetAsync(currentUserId);

            if (shoppingCart is null)
            {
                return new ServiceResult(ServiceResultType.NotFound, 
                    ExceptionConstants.NotFoundItemMessage);
            }

            order.UserId = new Guid(currentUserId);
            order.TotalPrice = shoppingCart.TotalPrice;
            order.ShoppingCartItems = shoppingCart.ShoppingCartItems;

            await _orderRepository.AddItemAsync(order);

            await _shoppingCartRepository.DeleteAsync(currentUserId);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(PagingParams pagingParams)
        {
            var userId = new Guid(_currentUserService.UserId);

            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId, pagingParams);

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<ServiceResult> DeleteOrderAsync(Guid id) =>
            await _orderRepository.DeleteOrderAsync(id);

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            return order is not null ? _mapper.Map<OrderDto>(order) : default;
        }
    }
}
