using Catalog.API.DAL.Entities;

namespace Catalog.UnitTests.Shared.Services
{
    public static class ProductRatingsTestData
    {
        public static ProductRating CreateProductRating() => new()
        {
            Product = CatalogServiceTestData.CreateProductEntity(),
            User = UsersServiceTestData.CreateUserEntity(),
            Rating = 5
        };
    }
}
