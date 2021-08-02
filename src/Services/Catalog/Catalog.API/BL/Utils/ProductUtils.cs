using Catalog.API.BL.Enums;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using System.Linq;

namespace Catalog.API.BL.Utils
{
    public static class ProductUtils
    {
        public static void FilterByCategory(ref IQueryable<Product> products, IProductRepository productRepository,
            string categoryName)
        {
            if (categoryName is not null)
            {
                products = productRepository
                    .GetQueryable(ref products, x => x.Category == categoryName);
            }
        }

        public static void FilterByAgeRating(ref IQueryable<Product> products, IProductRepository productRepository,
            int minimumAge)
        {
            if (minimumAge >= 0)
            {
                products = productRepository
                    .GetQueryable(ref products, x => (int)x.AgeRating >= minimumAge);
            }
        }

        public static void SortByPrice(ref IQueryable<Product> products, IProductRepository productRepository,
            OrderType? priceOrderType)
        {
            if (priceOrderType is not null)
            {
                productRepository.SortProductsByDefinition(ref products, priceOrderType,
                    t => t.Price);
            }
        }

        public static void SortByRating(ref IQueryable<Product> products, IProductRepository productRepository,
            OrderType? ratingOrderType)
        {
            if (ratingOrderType is not null)
            {
                productRepository.SortProductsByDefinition(ref products, ratingOrderType,
                    t => t.AverageRating);
            }
        }

        public static void SearchByName(ref IQueryable<Product> products, IProductRepository productRepository,
            string productName)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(productName))
                return;

            products = productRepository
                .GetQueryable(ref products, o => o.Name.ToLower().Contains(productName.Trim().ToLower()));
        }
    }
}
