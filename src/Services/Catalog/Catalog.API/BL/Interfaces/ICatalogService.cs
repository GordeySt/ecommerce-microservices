using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs;
using Catalog.API.PL.Models.Params;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        public Task<ServiceResult<Product>> AddProductAsync(CreateProductDto createProductDto);

        public Task<ServiceResult> DeleteProductAsync(Guid id);

        public Task<PagedList<ProductDto>> GetAllProductsAsync(PagingParams pagingParams);

        public Task<ProductDto> GetProductByIdAsync(Guid id);

        public Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto);
    }
}
