using Catalog.API.BL.Enums;
using Catalog.API.BL.Interfaces;
using Catalog.API.PL.DTOs;
using Catalog.API.PL.Models.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Services.Common.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        private readonly IPhotoService _photoService;

        public CatalogController(ICatalogService catalogService, 
            ILogger<CatalogController> logger, IPhotoService photoService)
        {
            _catalogService = catalogService;
            _logger = logger;
            _photoService = photoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProducts
            ([FromQuery] PagingParams pagingParams)
        {
            var products = await _catalogService.GetAllProductsAsync(pagingParams);

            return products;
        }

        [HttpGet("{id:guid}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await _catalogService.GetProductByIdAsync(id);

            if (product is null)
            {
                _logger.LogError($"Product with id: {id} not found.");
                return NotFound();
            }

            return product;
        }

        [Route("[action]", Name = "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByCategory
            ([FromQuery] CategoryParams categoryParams)
        {
            var products = await _catalogService.GetProductsByCategory(categoryParams);

            return products;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> CreateProduct
            ([BindRequired] CreateProductDto createProductDto)
        {
            await _catalogService.AddProductAsync(createProductDto);

            return CreatedAtAction(nameof(CreateProduct), createProductDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([BindRequired] UpdateProductDto updateProductDto)
        {
            var result = await _catalogService.UpdateProductAsync(updateProductDto);

            if (result.Result is ServiceResultType.NotFound)
            {
                _logger.LogError($"Product with id: {updateProductDto.Id} not found");
                return NotFound(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _catalogService.DeleteProductAsync(id);

            if (result.Result is ServiceResultType.NotFound)
            {
                _logger.LogError($"Product with id: {id} not found");
                return NotFound(result.Message);
            }

            return NoContent();
        }

        [HttpPost("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPhoto([FromForm(Name = "File")] IFormFile mainImage,
            Guid id)
        {
            var result = await _photoService.AddPhotoAsync(mainImage, id);

            if (result.Result is ServiceResultType.NotFound)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }
    }
}
