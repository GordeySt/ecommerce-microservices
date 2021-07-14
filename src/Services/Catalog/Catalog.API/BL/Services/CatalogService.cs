using AutoMapper;
using Catalog.API.BL.Interfaces;
using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.API.PL.DTOs;
using Catalog.API.PL.Models.Params;
using Services.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CatalogService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            await _productRepository.AddItemAsync(product);
        }
           
        public async Task<ServiceResult> DeleteProductAsync(Guid id) =>
            await _productRepository.DeleteItemAsync(id);

        public async Task<PagedList<ProductDto>> GetAllProductsAsync(PagingParams pagingParams)
        {
            var products = await _productRepository.GetAllItemsAsync(pagingParams);

            return _mapper.Map<PagedList<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var proudct = await _productRepository.GetItemByIdAsync(id);

            return proudct is not null ? _mapper.Map<ProductDto>(proudct) : default;
        }

        public async Task<PagedList<ProductDto>> GetProductsByCategory(CategoryParams categoryParams)
        {
            var products = await _productRepository.GetProductsByCategory(categoryParams);

            return _mapper.Map<PagedList<ProductDto>>(products);
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);

            return await _productRepository.UpdateItemAsync(product);
        }
    }
}
