using Basket.API.DAL.Enums;
using System;

namespace Basket.API.DAL.Entities
{
    public class ShoppingCartItem : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Category { get; set; }
        public string MainImageUrl { get; set; }
        public AgeRating AgeRating { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
