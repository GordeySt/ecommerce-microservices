using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs.ProductRatings;
using Catalog.API.PL.Models.DTOs.Users;
using Identity.Grpc.Protos;
using System;
using System.Collections.Generic;

namespace Catalog.Tests.Shared.Services
{
    public static class UsersServiceTestData
    {
        public static ApplicationUserModel CreateAppUserModel() => new()
        {
            Id = "f7434651-5d37-4657-989e-13cf3498c485",
            UserName = "TestUsername"
        };

        public static User CreateUserEntity() => new()
        {
            Id = new Guid("f7434651-5d37-4657-989e-13cf3498c485"),
            UserName = "TestUsername",
            Ratings = new List<ProductRating>()
            {
                new ProductRating
                {
                    Rating = 4,
                    Product = CatalogServiceTestData.CreateProductEntity()
                }
            }
        };

        public static UserDto CreateUserDto() => new()
        {
            Id = new Guid("f7434651-5d37-4657-989e-13cf3498c485"),
            UserName = "TestUsername",
            Ratings = new List<ProductRatingDto>()
            {
                new ProductRatingDto 
                {
                    Rating = 4,
                    Product = CatalogServiceTestData.CreateProductDto()
                }
            }
        };
    }
}
