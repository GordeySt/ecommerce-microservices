using Basket.API.DAL.Entities;
using Services.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IOrderRepository : IAsyncBaseRepository<Order>
    {
        public Task GetOrderById(Guid orderId);

        public Task<List<Order>> GetOrdersByUserIdAsync(Guid userId, PagingParams pagingParams);

        public Task DeleteItemAsync(Guid id);
    }
}
