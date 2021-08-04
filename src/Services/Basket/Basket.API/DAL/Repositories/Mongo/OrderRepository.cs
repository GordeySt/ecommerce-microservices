using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.PL.Models.DTOs;
using MongoDB.Driver;
using Services.Common.Models;
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

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId, PagingParams pagingParams) =>
            await PagedList<Order>.CreateAsync(databaseContext.Orders, 
                Builders<Order>.Filter.Eq(p => p.UserId, userId),
                pagingParams.PageNumber, pagingParams.PageSize);
    }
}
