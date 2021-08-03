using Basket.API.DAL.Entities;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Redis
{
    public interface IAsyncBaseRepository<T> where T : EntityBase
    {
        public Task AddAsync(T shoppingCart);

        public Task DeleteAsync(string cacheKey);

        public Task<T> GetAsync(string cacheKey);
    }
}
