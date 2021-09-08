using Catalog.API.BL.Interfaces;
using Catalog.API.PL.Filters;
using Catalog.API.PL.Models.DTOs.ProductRatings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductRatingsController : ControllerBase
    {
        private readonly IProductRatingsService _productRatingsService;

        public ProductRatingsController(IProductRatingsService productRatingsService)
        {
            _productRatingsService = productRatingsService;
        }

        /// <summary>
        /// Rate product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/add-ratings/id/e0c40c25-52f7-48df-91fc-441dadca0a0f
        /// 
        /// </remarks>
        /// <returns>Returns NoContent Object Result</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If rating already exists</response>
        /// <response code="404">If product rating not found (user or product not found)</response>
        [HttpPost("add-ratings")]
        /*[ProductRatingParamFilter]*/
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddRatingToProduct(AddRatingsDto addRatings)
        {
            var result = await _productRatingsService
                .AddRatingToProductAsync(addRatings.Id, addRatings.RatingCount);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Update product rating
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/change-ratings/id/e0c40c25-52f7-48df-91fc-441dadca0a0f
        /// 
        /// </remarks>
        /// <returns>Returns NoContent Object Result</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If product rating not found (user or product not found)</response>
        [HttpPost("change-ratings")]
        /*[ProductRatingParamFilter]*/
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeRatingAtProduct(AddRatingsDto addRatings)
        {
            var result = await _productRatingsService
                .UpdateRatingAtProductAsync(addRatings.Id, addRatings.RatingCount);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return NoContent();
        }
    }
}