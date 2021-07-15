using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.Validation;
using System;

namespace Catalog.API.PL.DTOs
{
    public class CreateProductDto : IMapFrom<CreateProductDto>
    {
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

        [DefaultValue]
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, Product>();
        }
    }
}
