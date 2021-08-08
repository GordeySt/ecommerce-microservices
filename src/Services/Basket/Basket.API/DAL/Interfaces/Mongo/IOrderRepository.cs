using Basket.API.DAL.Entities;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IOrderRepository : IAsyncBaseRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);

        Task<List<Order>> GetOrdersByUserIdAsync(Guid userId, PagingParams pagingParams);

        Task<ServiceResult> DeleteOrderAsync(Guid id);
    }
}
