using Basket.API.DAL.Entities;
using Basket.API.DAL.Enums;
using System;
using System.Collections.Generic;

namespace Basket.Tests.Shared.Services
{
    public static class ShoppingCartTestData
    {
        public const int CorrectTotalPrice = 80;

        public static ShoppingCart CreateShoppingCartEntity() => new()
        {
            ShoppingCartItems = new List<ShoppingCartItem>
            {
                CreateShoppingCartItem(),
                CreateShoppingCartItem()
            }
        };

        public static ShoppingCartItem CreateShoppingCartItem() => new()
        {
            Id = Guid.NewGuid(),
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
