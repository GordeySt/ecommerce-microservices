using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.PL.Models.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.DAL.Repositories.Mongo
{
    public class OrderRepository : AsyncBaseRepository<Order>,
        IOrderRepository
    {
        public OrderRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public Task DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetOrderById(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrderByUserIdAsync(Guid userId) =>
            await databaseContext
                .Orders
                .Find(p => p.UserId == userId)
                .ToListAsync();
    }
}
