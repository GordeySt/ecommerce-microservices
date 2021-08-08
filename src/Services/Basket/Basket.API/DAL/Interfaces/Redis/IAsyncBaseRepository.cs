using Basket.API.DAL.Entities;
using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Redis
{
    public interface IAsyncBaseRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T shoppingCart);

        Task DeleteAsync(string cacheKey);

        Task<T> GetAsync(string cacheKey);
    }
}
