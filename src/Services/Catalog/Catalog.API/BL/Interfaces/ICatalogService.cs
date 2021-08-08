using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs.Products;
using Catalog.API.PL.Models.Params;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        Task<ServiceResult<Product>> AddProductAsync(CreateProductDto createProductDto);

        Task<ServiceResult> DeleteProductAsync(Guid id);

        Task<PagedList<ProductDto>> GetAllProductsAsync(ProductsParams productsParams);

        Task<List<string>> GetPopularCategoriesAsync(int populerCategoriesCount);

        Task<ProductDto> GetProductByIdAsync(Guid id);

        Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto);
    }
}
