using Catalog.API.BL.ResultWrappers;
using Catalog.API.PL.DTOs;
using Catalog.API.PL.Models.Params;
using Services.Common.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        Task<PagedList<ProductDto>> GetAllProductsAsync(PagingParams pagingParams);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<PagedList<ProductDto>> GetProductsByCategory(CategoryParams categoryParams);
        Task AddProductAsync(CreateProductDto createProductDto);
        Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<ServiceResult> DeleteProductAsync(Guid id);
    }
}
