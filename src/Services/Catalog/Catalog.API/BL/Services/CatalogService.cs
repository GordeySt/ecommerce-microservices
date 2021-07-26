using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.API.BL.Interfaces;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.API.PL.Models.DTOs;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.Models;
using Services.Common.ResultWrappers;
using System;
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
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionConstants.NotFoundItemMessage);
            }

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<PagedList<ProductDto>> GetAllProductsAsync(PagingParams pagingParams)
        {
            var products = _productRepository.GetAllQueryable();

            var productsDto = products
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

            return await PagedList<ProductDto>.CreateAsync(productsDto, pagingParams.PageNumber,
                pagingParams.PageSize);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var proudct = await _productRepository.GetByIdAsync(id);

            return proudct is not null ? _mapper.Map<ProductDto>(proudct) : default;
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetByIdAsync(updateProductDto.Id, false);

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
