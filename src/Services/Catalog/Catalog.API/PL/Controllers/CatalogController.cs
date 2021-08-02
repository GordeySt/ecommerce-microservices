using Catalog.API.BL.Interfaces;
using Catalog.API.PL.Filters;
using Catalog.API.PL.Filters.ResponseCaching;
using Catalog.API.PL.Models.DTOs.Products;
using Catalog.API.PL.Models.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Services.Common.Enums;
using Services.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        private readonly IPhotoService _photoService;

        public CatalogController(ICatalogService catalogService, ILogger<CatalogController> logger, 
            IPhotoService photoService)
        {
            _catalogService = catalogService;
            _logger = logger;
            _photoService = photoService;
        }

        /// <summary>
        /// Gets the most popular categories of products
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/get-popular?popularCategoriesCount=2
        /// 
        /// </remarks>
        /// <param name="popularCategoriesCount">Count of the popular categories</param>
        /// <returns>Returns PagedList of ProductDto</returns>
        /// <response code="200">Success</response>
        [HttpGet("get-popular")]
        [PopularCategoriesParamFilter]
        [CachedFilter(600)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetPopularCategories
            ([FromQuery] int popularCategoriesCount) => 
            (await _catalogService.GetPopularCategoriesAsync(popularCategoriesCount)).ToList();



        /// <summary>
        /// Gets the paginated list of products
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Catalog?pageNumber=4&amp;pageSize=4
        /// 
        /// </remarks>
        /// <param name="productsParams"></param>
        /// <returns>Returns PagedList of ProductDto</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProducts
            ([FromQuery] ProductsParams productsParams)
            => await _catalogService.GetAllProductsAsync(productsParams);

        /// <summary>
        /// Gets the product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Catalog/e0c40c25-52f7-48df-91fc-441dadca0a0f
        /// 
        /// </remarks>
        /// <param name="id">Note id (guid)</param>
        /// <returns>Returns ProductDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the product not found</response>
        [HttpGet("{id:guid}", Name = "GetProduct")]
        [CachedFilter(600)]
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

        /// <summary>
        /// Creates the product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Catalog
        ///     { 
        ///         name: "f",
        ///         category: "Computers",
        ///         summary: "Summary",
        ///         description: "Description",
        ///         price: 54.93 
        ///     }
        /// 
        /// </remarks>
        /// <param name="createProductDto">CreateProductDto object</param>
        /// <returns>Returns CreatedAtAction with CreateProductDto object</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [EraseCacheFilter]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> CreateProduct
            ([BindRequired] CreateProductDto createProductDto)
        {
            var creationResult = await _catalogService.AddProductAsync(createProductDto);

            if (creationResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)creationResult.Result, creationResult.Message);
            }

            return CreatedAtAction(nameof(CreateProduct), creationResult.Data);
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Catalog
        ///     { 
        ///         id: "0e39f30d-4754-48c0-8191-755f673e2269"
        ///         name: "f",
        ///         category: "Computers",
        ///         summary: "Summary",
        ///         description: "Description",
        ///         price: 54.93 
        ///     }
        /// 
        /// </remarks>
        /// <param name="updateProductDto">UpdateProductDto object</param>
        /// <returns>Returns NoContent Result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If the product not found</response>
        [HttpPut]
        [EraseCacheFilter]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([BindRequired] UpdateProductDto updateProductDto)
        {
            var result = await _catalogService.UpdateProductAsync(updateProductDto);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Catalog/dba6469d-db47-4b57-b1d0-2b0f0a9a06d9
        ///     
        /// </remarks>
        /// <param name="id">id of the product (guid)</param>
        /// <returns>Return NoContent Result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If the product not found</response>
        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        [EraseCacheFilter]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _catalogService.DeleteProductAsync(id);

            if (result.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)result.Result, result.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Add photo to the product by productId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Catalog/0e39f30d-4754-48c0-8191-755f673e2269/AddPhotoToCategory
        ///     
        /// </remarks>
        /// <param name="mainImage">Image file (IFormFile)</param>
        /// <param name="id">id of the product to add photo (guid)</param>
        /// <returns>Returns NoContent Result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If the product with id (guid) not found</response>
        [HttpPost("add-photo/id/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPhotoToProduct([FromForm(Name = "File")] IFormFile mainImage,
            Guid id)
        {
            var addPhotoResult = await _photoService.AddPhotoAsync(mainImage, id);

            if (addPhotoResult.Result is not ServiceResultType.NotFound)
            {
                return StatusCode((int)addPhotoResult.Result, addPhotoResult.Message);
            }

            return NoContent();
        }
    }
}
