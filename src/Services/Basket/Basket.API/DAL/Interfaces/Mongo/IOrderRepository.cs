using Basket.API.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IOrderRepository<T> : IAsyncBaseRepository<Order>
    {
        public Task GetOrderById(Guid orderId);

        public Task GetOrderByUserId(Guid userId);

        public Task DeleteItemAsync(Guid id);
    }
}
