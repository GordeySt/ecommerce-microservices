using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Enums;
using Catalog.API.PL.Models.DTOs.Products;
using System;
using System.Collections.Generic;

namespace Catalog.Tests.Shared.Services
{
    public static class CatalogServiceTestData
    {
        public static CreateProductDto CreateCreateProductDto() => new()
        {
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Count = 10,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10
        };

        public static UpdateProductDto CreateUpdateProductDto() => new()
        {
            Id = new Guid("38b4afa4-9c15-4d8e-8011-8f4576694d04"),
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Count = 10,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10
        };

        public static Product CreateProductEntity() => new()
        {
            Id = new Guid("38b4afa4-9c15-4d8e-8011-8f4576694d04"),
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Count = 10,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10,
            AverageRating = 0,
            TotalRating = 0,
            MainImageUrl = "TestUrl",
            Ratings = new List<ProductRating>()
            {
                new ProductRating
                {
                    Rating = 4
                }
            }
        };

        public static ProductDto CreateProductDto() => new()
        {
            Id = new Guid("38b4afa4-9c15-4d8e-8011-8f4576694d04"),
            Category = "TestCategory",
            AgeRating = 3,
            Count = 10,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10,
            AverageRating = 0,
            MainImageUrl = "TestUrl"
        };

        public static List<string> GetPopularCategories(int popularCategoriesCount)
        {
            var popularCategories = new List<string>();

            for (int i = 0; i < popularCategoriesCount; i++)
            {
                popularCategories.Add(Guid.NewGuid().ToString());
            }

            return popularCategories;
        }
    }
}
