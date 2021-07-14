using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await _collection
                .Find(_ => true)
                .ToListAsync();
        }

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

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _collection
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateItemAsync(T entity)
        {
            var updateResult = await _collection
                .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
