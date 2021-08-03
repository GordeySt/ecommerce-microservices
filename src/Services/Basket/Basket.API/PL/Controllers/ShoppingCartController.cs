using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingCart(ShoppingCart shoppingCart)
        {
            var result = await _shoppingCartService.AddShoppingCartAsync(shoppingCart);

            return CreatedAtAction(nameof(AddShoppingCart), result);
        }
    }
}
