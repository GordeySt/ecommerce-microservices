﻿using System;
using System.Collections.Generic;

namespace Basket.API.PL.Models.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
