using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using Services.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutShoppingCart(Order order)
        {
            var result = await _orderService.CheckoutShoppingCartAsync(order);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return NoContent();
        }

        [HttpGet("get-by-userid")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersAsync([FromQuery] PagingParams pagingParams)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(pagingParams);

            if (orders is null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.DeleteOrderAsync(id);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }    

            return NoContent();
        }
    }
}
