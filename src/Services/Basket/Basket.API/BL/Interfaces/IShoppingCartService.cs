using Basket.API.DAL.Entities;
using System.Threading.Tasks;

namespace Basket.API.BL.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart);
    }
}
