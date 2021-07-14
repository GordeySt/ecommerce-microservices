using Catalog.API.BL.Enums;
using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogService catalogService, ILogger<CatalogController> logger)
        {
            _catalogService = catalogService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _catalogService.GetAllProductsAsync();

            return products.ToList();
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

        [Route("[action]/{categoryName}", Name = "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string categoryName)
        {
            var products = await _catalogService.GetProductsByCategory(categoryName);

            return products.ToList();
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
                _logger.LogInformation($"Product with id: {updateProductDto.Id} not found");
                return NotFound(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _catalogService.DeleteProductAsync(id);

            if (result.Result is ServiceResultType.NotFound)
            {
                _logger.LogInformation($"Product with id: {id} not found");
                return NotFound(result.Message);
            }

            return NoContent();
        }
    }
}
