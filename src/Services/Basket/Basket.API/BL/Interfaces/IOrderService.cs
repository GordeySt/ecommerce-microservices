using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.BL.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult> CheckoutShoppingCartAsync(Order order);

        Task<List<OrderDto>> GetOrdersByUserIdAsync(PagingParams pagingParams);

        Task<ServiceResult> DeleteOrderAsync(Guid id);

        Task<ServiceResult<OrderDto>> GetOrderByIdAsync(Guid orderId);
    }
}
