using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.API.BL.Enums;
using Catalog.API.BL.Interfaces;
using Catalog.API.BL.Utils;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.API.PL.Models.DTOs.Products;
using Catalog.API.PL.Models.Params;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ServiceResult<Product>> AddProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            return await _productRepository.AddAsync(product);
        }
           
        public async Task<ServiceResult> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionConstants.NotFoundItemMessage);
            }

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<List<string>> GetPopularCategoriesAsync(int populerCategoriesCount) => 
            await _productRepository.GetPopularCategoriesAsync(populerCategoriesCount);

        public async Task<PagedList<ProductDto>> GetAllProductsAsync(ProductsParams productsParams)
        {
            var products = _productRepository.GetAllQueryable();

            ProductUtils.FilterByCategory(ref products, _productRepository, productsParams.CategoryName);

            ProductUtils.FilterByAgeRating(ref products, _productRepository, (int)productsParams.MinimumAge);

            ProductUtils.SortByPrice(ref products, _productRepository, productsParams.PriceOrderType);

            ProductUtils.SortByRating(ref products, _productRepository, productsParams.RatingOrderType);

            ProductUtils.SearchByName(ref products, _productRepository, productsParams.ProductName);

            _productRepository.SortProductsByDefinition(ref products, OrderType.Asc, t => t.Id);

            var productsDto = products
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

            return await PagedList<ProductDto>.CreateAsync(productsDto, productsParams.PageNumber,
                productsParams.PageSize);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id, disableTracking: false);

            return product is not null ? _mapper.Map<ProductDto>(product) : default;
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(updateProductDto.Id, disableTracking: false);

            if (product is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionConstants.NotFoundItemMessage);
            }

            var productToUpdate = _mapper.Map<Product>(updateProductDto);

            return await _productRepository.UpdateAsync(productToUpdate);
        }
    }
}
