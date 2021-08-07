using Basket.API.DAL.Entities;
using Services.Common.Models;
using System;
using System.Collections.Generic;

namespace Basket.UnitTests.Shared.Services
{
    public static class OrderTestData
    {
        public static Order CreateOrderEntity() => new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ShoppingCartItems = new List<ShoppingCartItem>
            {
                ShoppingCartTestData.CreateShoppingCartItem(),
                ShoppingCartTestData.CreateShoppingCartItem()
            },
            TotalPrice = ShoppingCartTestData.CorrectTotalPrice
        };

        public static List<Order> CreateOrdersWithPagingParams(PagingParams pagingParams)
        {
            var orders = new List<Order>();

            for (int i = 0; i < pagingParams.PageSize; i++)
            {
                orders.Add(CreateOrderEntity());
            }

            return orders;
        }
    }
}
