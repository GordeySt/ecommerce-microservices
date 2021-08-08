using Basket.API.DAL.Entities;
using Basket.API.DAL.Enums;
using Basket.API.PL.Models.DTOs;
using System;
using System.Collections.Generic;

namespace Basket.UnitTests.Shared.Services
{
    public static class ShoppingCartTestData
    {
        public const int CorrectTotalPrice = 80;

        public static ShoppingCart CreateShoppingCartEntity(Guid id) => new()
        {
            Id = id,
            ShoppingCartItems = new List<ShoppingCartItem>
            {
                CreateShoppingCartItem(),
                CreateShoppingCartItem()
            }
        };

        public static AddShoppingCartDto CreateAddShoppingCartDto() => new()
        {
            ShoppingCartItems = new List<ShoppingCartItemDto>
            {
                CreateShoppingCartItemDto(),
                CreateShoppingCartItemDto()
            }
        };

        public static ShoppingCartDto CreateShoppingCartDto(Guid id) => new()
        {
            Id = id,
            TotalPrice = CorrectTotalPrice,
            ShoppingCartItems = new List<ShoppingCartItemDto>
            {
                CreateShoppingCartItemDto(),
                CreateShoppingCartItemDto()
            }
        };

        private static ShoppingCartItemDto CreateShoppingCartItemDto() => new()
        {
            Id = new Guid("edbf4592-f282-4cfe-afc8-1204a8231549"),
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Quantity = 4,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10,
            MainImageUrl = "TestUrl"
        };

        public static ShoppingCartItem CreateShoppingCartItem() => new()
        {
            Id = new Guid("edbf4592-f282-4cfe-afc8-1204a8231549"),
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Quantity = 4,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10,
            MainImageUrl = "TestUrl"
        };
    }
}
