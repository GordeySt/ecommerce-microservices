using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.Validation;
using System;

namespace Catalog.API.PL.Models.DTOs
{
    public class UpdateProductDto : IMapFrom<UpdateProductDto>
    {
        /// <summary>
        /// Id (guid) of the product
        /// </summary>
        /// <example>0e39f30d-4754-48c0-8191-755f673e2269</example>
        [DefaultValue]
        public Guid Id { get; set; }

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

        /// <summary>
        /// Age Rating of the product
        /// </summary>
        /// <example>6</example>
        [DefaultValue]
        public int AgeRating { get; set; }

        /// <summary>
        /// Count of the product
        /// </summary>
        /// <example>10</example>
        [DefaultValue]
        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, Product>();
        }
    }
}
