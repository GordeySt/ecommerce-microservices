using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs;
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
        public Task<ServiceResult<Product>> AddProductAsync(CreateProductDto createProductDto);

        public Task<ServiceResult> DeleteProductAsync(Guid id);

        public Task<PagedList<ProductDto>> GetAllProductsAsync(ProductsParams productsParams);

        public Task<IEnumerable<string>> GetPopularCategoriesAsync(int populerCategoriesCount);

        public Task<ProductDto> GetProductByIdAsync(Guid id);

        public Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto);
    }
}
