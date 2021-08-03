using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using System.Threading.Tasks;

namespace Basket.API.BL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart)
        {
            await _shoppingCartRepository.AddAsync(shoppingCart);

            return shoppingCart;
        }
    }
}
