using System;

namespace Catalog.API.PL.Models.DTOs.ProductRatings
{

    public class AddRatingsDto
    {
        public Guid Id { get; set; }
        public int RatingCount { get; set; }
    }

}
