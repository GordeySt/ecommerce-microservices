using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.API.BL.Enums;
using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.API.PL.Models.DTOs;
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

        public async Task<IEnumerable<string>> GetPopularCategoriesAsync(int populerCategoriesCount) => 
            await _productRepository.GetPopularCategoriesAsync(populerCategoriesCount);

        public async Task<PagedList<ProductDto>> GetAllProductsAsync(ProductsParams productsParams)
        {
            var products = _productRepository.GetAllQueryable();

            FilterByCategory(ref products, productsParams.CategoryName);

            FilterByAgeRating(ref products, (int)productsParams.MinimumAge);

            SortByPrice(ref products, productsParams.PriceOrderType);

            SearchByName(ref products, productsParams.ProductName);

            var productsDto = products
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

            return await PagedList<ProductDto>.CreateAsync(productsDto, productsParams.PageNumber,
                productsParams.PageSize);
        }

        private void FilterByCategory(ref IQueryable<Product> products, string categoryName)
        {
            if (categoryName is not null)
            {
                products = _productRepository
                    .GetQueryable(ref products, x => x.Category == categoryName);
            }
        }

        private void FilterByAgeRating(ref IQueryable<Product> products, int minimumAge)
        {
            if (minimumAge >= 0)
            {
                products = _productRepository
                    .GetQueryable(ref products, x => (int)x.AgeRating >= minimumAge);
            }
        }
        private void SortByPrice(ref IQueryable<Product> products, PriceOrderType? priceOrderType)
        {
            if (priceOrderType is not null)
            {
                products = _productRepository
                    .SortProductsByPrice(ref products, priceOrderType);
            }
        }

        private void SearchByName(ref IQueryable<Product> products, string productName)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(productName))
                return;

            products = _productRepository
                .GetQueryable(ref products, o => o.Name.ToLower().Contains(productName.Trim().ToLower()));
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var proudct = await _productRepository.GetProductByIdAsync(id);

            return proudct is not null ? _mapper.Map<ProductDto>(proudct) : default;
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(updateProductDto.Id, false);

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
