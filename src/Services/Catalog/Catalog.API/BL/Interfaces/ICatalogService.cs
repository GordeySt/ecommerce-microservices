﻿using Catalog.API.PL.Models.DTOs;
using Catalog.API.PL.Models.Params;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface ICatalogService
    {
        Task<PagedList<ProductDto>> GetAllProductsAsync(PagingParams pagingParams);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<PagedList<ProductDto>> GetProductsByCategoryAsync(CategoryParams categoryParams);
        Task AddProductAsync(CreateProductDto createProductDto);
        Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<ServiceResult> DeleteProductAsync(Guid id);
    }
}
