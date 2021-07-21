using Catalog.API.BL.Interfaces;
using Catalog.API.PL.Models.DTOs;
using Catalog.API.PL.Models.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Services.Common.Enums;
using Services.Common.Models;
using System;
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

        public CatalogController(ICatalogService catalogService, 
            ILogger<CatalogController> logger, IPhotoService photoService)
        {
            _catalogService = catalogService;
            _logger = logger;
            _photoService = photoService;
        }

        /// <summary>
        /// Gets the paginated list of products
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Catalog?pageNumber=4&amp;pageSize=4
        /// 
        /// </remarks>
        /// <param name="pagingParams"></param>
        /// <returns>Returns PagedList of ProductDto</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProducts
            ([FromQuery] PagingParams pagingParams) => await _catalogService.GetAllProductsAsync(pagingParams);

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
        /// Gets the products by category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Catalog/GetProductsByCategory?categoryName=Computers&amp;pageNumber=2&amp;pageSize=3
        ///     
        /// </remarks>
        /// <param name="categoryParams">CategoryParams object including PagingParams object</param>
        /// <returns>Returns PagedList of ProductDto</returns>
        /// <response code="200">Success</response>
        [Route("[action]", Name = "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByCategory
            ([FromQuery] CategoryParams categoryParams) => 
            await _catalogService.GetProductsByCategoryAsync(categoryParams);

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> CreateProduct
            ([BindRequired] CreateProductDto createProductDto)
        {
            await _catalogService.AddProductAsync(createProductDto);

            return CreatedAtAction(nameof(CreateProduct), createProductDto);
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
        [HttpPost("{id:guid}/AddPhotoToProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPhotoToProduct([FromForm(Name = "File")] IFormFile mainImage,
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
