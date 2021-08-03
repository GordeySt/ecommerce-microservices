using System;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IAsyncBaseRepository<T> where T : class
    {
        public Task AddItemAsync(T entity);
    }
}
