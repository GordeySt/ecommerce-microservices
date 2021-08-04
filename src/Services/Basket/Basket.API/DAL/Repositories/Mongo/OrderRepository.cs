using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.PL.Models.DTOs;
using MongoDB.Driver;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.Models;
using Services.Common.ResultWrappers;
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

        public async Task<ServiceResult> DeleteOrderAsync(Guid id)
        {
            var result = await databaseContext
                .Orders
                .DeleteOneAsync(Builders<Order>.Filter.Eq(p => p.Id, id));

            if (result.DeletedCount == 0)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionConstants.NotFoundItemMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId) =>
            await databaseContext
                .Orders
                .Find(p => p.Id == orderId)
                .FirstOrDefaultAsync();

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId, PagingParams pagingParams) =>
            await PagedList<Order>.CreateAsync(databaseContext.Orders, 
                Builders<Order>.Filter.Eq(p => p.UserId, userId),
                pagingParams.PageNumber, pagingParams.PageSize);
    }
}
