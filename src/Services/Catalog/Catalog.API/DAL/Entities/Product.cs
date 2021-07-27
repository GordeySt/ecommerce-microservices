using Catalog.API.DAL.Enums;
using System.Collections.Generic;

namespace Catalog.API.DAL.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Category { get; set; }
        public string MainImageUrl { get; set; }
        public AgeRating AgeRating { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public int TotalRating { get; set; }
    }
}
