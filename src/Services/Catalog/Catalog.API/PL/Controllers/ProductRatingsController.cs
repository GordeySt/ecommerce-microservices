using Catalog.API.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductRatingsController : ControllerBase
    {
        private readonly IProductRatingsService _productRatingsService;

        public ProductRatingsController(IProductRatingsService productRatingsService)
        {
            _productRatingsService = productRatingsService;
        }

        [HttpPost("add-ratings/id/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> AddRatingToProduct(Guid id, [FromQuery] int ratingCount)
        {
            var result = await _productRatingsService.AddRatingToProductAsync(id, ratingCount);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return NoContent();
        }
    }
}
