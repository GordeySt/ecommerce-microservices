using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
using Services.Common.ResultWrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.BL.Interfaces
{
    public interface IOrderService
    {
        public Task<ServiceResult> CheckoutShoppingCartAsync(Order order);

        public Task<List<OrderDto>> GetOrderByUserIdAsync();
    }
}
