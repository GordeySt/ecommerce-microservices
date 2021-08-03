using System;
using System.Collections.Generic;

namespace Basket.API.DAL.Entities
{
    public class ShoppingCart : EntityBase
    { 
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in ShoppingCartItems)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }
        }
    }
}