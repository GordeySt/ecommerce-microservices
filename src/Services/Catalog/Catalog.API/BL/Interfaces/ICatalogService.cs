using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetProductsByCategory(string categoryName);
        Task AddProductAsync(CreateProductDto createProductDto);
        Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<ServiceResult> DeleteProductAsync(Guid id);
    }
}
