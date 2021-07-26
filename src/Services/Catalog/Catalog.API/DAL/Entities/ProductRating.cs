using System;

namespace Catalog.API.DAL.Entities
{
    public class ProductRating
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Rating { get; set; }
    }
}
