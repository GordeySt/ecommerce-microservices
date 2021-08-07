using Basket.API.DAL.Entities;

namespace Basket.API.DAL.Interfaces.Redis
{
    public interface IShoppingCartRepository : IAsyncBaseRepository<ShoppingCart>
    {
    }
}
