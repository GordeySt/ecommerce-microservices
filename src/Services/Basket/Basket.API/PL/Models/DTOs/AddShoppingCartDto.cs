using System;
using System.Collections.Generic;

namespace Basket.API.PL.Models.DTOs
{
    public class AddShoppingCartDto
    {
        public Guid Id { get; set; }
        public ICollection<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
