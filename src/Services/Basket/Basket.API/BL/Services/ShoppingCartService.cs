using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using System;
using System.Threading.Tasks;

namespace Basket.API.BL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICurrentUserService _currentUserService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, 
            ICurrentUserService currentUserService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ShoppingCart> AddShoppingCartAsync(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = new Guid(_currentUserService.UserId);

            return await _shoppingCartRepository.AddAsync(shoppingCart);
        }
    }
}
