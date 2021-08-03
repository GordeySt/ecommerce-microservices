using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Basket.API.DAL.Entities
{
    public class Order
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public Payment Payment { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
