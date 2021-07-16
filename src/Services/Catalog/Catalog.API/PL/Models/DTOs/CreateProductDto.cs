using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.Validation;
using System;

namespace Catalog.API.PL.DTOs
{
    public class CreateProductDto : IMapFrom<CreateProductDto>
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        /// <example>IPhone X</example>
        [DefaultValue]
        public string Name { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        /// <example>Tim Cooks child</example>
        [DefaultValue]
        public string Description { get; set; }

        /// <summary>
        /// Summary about the product
        /// </summary>
        /// <example>New Smart Phone</example>
        [DefaultValue]
        public string Summary { get; set; }

        /// <summary>
        /// Category of the product
        /// </summary>
        /// <example>Smart Phones</example>
        [DefaultValue]
        public string Category { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        /// <example>60</example>
        [DefaultValue]
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, Product>();
        }
    }
}
