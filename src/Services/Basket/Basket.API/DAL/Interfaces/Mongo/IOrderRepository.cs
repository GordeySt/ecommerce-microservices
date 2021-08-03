using Basket.API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IOrderRepository : IAsyncBaseRepository<Order>
    {
        public Task GetOrderById(Guid orderId);

        public Task<List<Order>> GetOrderByUserIdAsync(Guid userId);

        public Task DeleteItemAsync(Guid id);
    }
}
