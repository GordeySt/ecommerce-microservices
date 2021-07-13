using Catalog.API.BL.Interface;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _productRepository;

        public CatalogService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(Product product) => 
            await _productRepository.AddItemAsync(product);

        public async Task<bool> DeleteProductAsync(Guid id) =>
            await _productRepository.DeleteItemAsync(id);

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync() =>
            await _productRepository.GetAllItemsAsync();


        public async Task<Product> GetProductByIdAsync(Guid id) =>
            await _productRepository.GetItemByIdAsync(id);

        public async Task<IReadOnlyCollection<Product>> GetProductsByCategory(string categoryName) =>
            await _productRepository.GetProductsByCategory(categoryName);

        public async Task<bool> UpdateProductAsync(Product product) =>
            await _productRepository.UpdateItemAsync(product);
    }
}
