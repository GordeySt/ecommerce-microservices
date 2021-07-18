using Catalog.API.BL.Constants;
using Catalog.API.BL.Enums;
using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using MongoDB.Driver;
using Services.Common.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class AsyncBaseRepository<T> : IAsyncBaseRepository<T> where T 
        : EntityBase
    {
        protected readonly IDatabaseContext DatabaseContext;
        private readonly IMongoCollection<T> _collection;

        public AsyncBaseRepository(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
            _collection = DatabaseContext.GetCollection<T>(typeof(T).Name);
        }

        public async Task<PagedList<T>> GetAllItemsAsync(PagingParams pagingParams) => 
            await PagedList<T>.CreateAsync(_collection, Builders<T>.Filter.Empty, 
                pagingParams.PageNumber, pagingParams.PageSize);

        public async Task<T> GetItemByIdAsync(Guid id)
        {
            return await _collection
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddItemAsync(T entity)
        {
            try
            {
                await _collection
                    .InsertOneAsync(entity);
            }
            catch
            {
                throw new Exception("Problem inserting data");
            }

        }

        public async Task<ServiceResult> DeleteItemAsync(Guid id)
        {
            var itemToDelete = await _collection
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (itemToDelete is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var filter = Builders<T>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _collection
                .DeleteOneAsync(filter);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> UpdateItemAsync(T entity)
        {
            var itemToUpdate = await _collection
                .Find(p => p.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (itemToUpdate is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            var updateResult = await _collection
                .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
