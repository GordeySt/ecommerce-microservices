using Basket.API.BL.Interfaces;
using Basket.API.PL.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System.Threading.Tasks;

namespace Basket.API.PL.Controllers
{
    [ApiController]
    [Route("api/shopping-cart")]
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

        [HttpGet]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartById()
        {
            var result = await _shoppingCartService.GetShoppingCartByIdAsync();

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return result.Data;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShoppingCart()
        {
            await _shoppingCartService.DeleteShoppingCartAsync();

            return NoContent();
        }
    }
}
