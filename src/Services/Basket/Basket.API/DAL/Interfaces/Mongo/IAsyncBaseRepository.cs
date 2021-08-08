using System.Threading.Tasks;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IAsyncBaseRepository<T> where T : class
    {
        Task AddItemAsync(T entity);
    }
}
