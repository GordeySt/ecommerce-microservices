using Basket.API.DAL.Interfaces.Mongo;
using MongoDB.Driver;
using Services.Common.Constatns;
using System;
using System.Threading.Tasks;

namespace Basket.API.DAL.Repositories.Mongo
{
    public abstract class AsyncBaseRepository<T> : IAsyncBaseRepository<T> where T :
        class
    {
        protected readonly IDatabaseContext DatabaseContext;
        private readonly IMongoCollection<T> _collection;

        protected AsyncBaseRepository(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
            _collection = DatabaseContext.GetCollection<T>(typeof(T).Name);
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
                throw new Exception(ExceptionConstants.ProblemCreatingItemMessage);
            }
        }
    }
}
