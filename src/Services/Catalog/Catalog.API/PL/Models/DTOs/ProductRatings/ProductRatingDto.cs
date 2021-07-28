using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs.Products;

namespace Catalog.API.PL.Models.DTOs.ProductRatings
{
    public class ProductRatingDto : IMapFrom<ProductRating>
    {
        public int Rating { get; set; }
        public ProductDto Product { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductRating, ProductRatingDto>()
                .ForMember(d => d.Product, options => options.MapFrom(source => source.Product));
        }
    }
}
