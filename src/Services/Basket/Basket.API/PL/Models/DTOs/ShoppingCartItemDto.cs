using Basket.API.DAL.Enums;
using Basket.API.PL.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.PL.Models.DTOs
{
    public class ShoppingCartItemDto
    {
        public Guid Id { get; set; }
        
        [DefaultValue]
        public string Name { get; set; }

        [DefaultValue]
        public string Description { get; set; }

        [DefaultValue]
        public string Summary { get; set; }

        [DefaultValue]
        public string Category { get; set; }

        [DefaultValue]
        public string MainImageUrl { get; set; }

        [Required]
        public AgeRating AgeRating { get; set; }

        [DefaultValue]
        public int Quantity { get; set; }

        [DefaultValue]
        public decimal Price { get; set; }
    }
}
