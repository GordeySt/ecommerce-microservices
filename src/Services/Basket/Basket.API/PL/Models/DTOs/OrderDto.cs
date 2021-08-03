using Basket.API.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Basket.API.PL.Models.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;
        public BillingAddress BillingAddress { get; set; }
        public Payment Payment { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
