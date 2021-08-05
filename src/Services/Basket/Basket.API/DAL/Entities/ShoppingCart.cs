using System.Collections.Generic;
using System.Linq;

namespace Basket.API.DAL.Entities
{
    public class ShoppingCart : EntityBase
    { 
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice => ShoppingCartItems.Sum(item => item.Price * item.Quantity);
    }
}