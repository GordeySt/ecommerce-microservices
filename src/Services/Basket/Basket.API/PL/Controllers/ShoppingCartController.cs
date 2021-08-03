using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
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
        public async Task<IActionResult> AddShoppingCart(AddShoppingCartDto addShoppingCartDto)
        {
            var result = await _shoppingCartService.AddShoppingCartAsync(addShoppingCartDto);

            return CreatedAtAction(nameof(AddShoppingCart), result);
        }
    }
}
