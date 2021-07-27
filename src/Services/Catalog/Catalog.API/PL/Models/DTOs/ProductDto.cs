using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.DAL.Entities;
using System;

namespace Catalog.API.PL.Models.DTOs
{
    public class ProductDto : IMapFrom<Product>
    {
        /// <summary>
        /// Id (guid) of the product
        /// </summary>
        /// <example>0e39f30d-4754-48c0-8191-755f673e2269</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        /// <example>IPhone X</example>
        public string Name { get; set; }

        /// <summary>
        /// Category of the product
        /// </summary>
        /// <example>Smart Phones</example>
        public string Category { get; set; }

        /// <summary>
        /// Summary about the product
        /// </summary>
        /// <example>New Smart Phone</example>
        public string Summary { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        /// <example>Tim Cooks child</example>
        public string Description { get; set; }

        /// <summary>
        /// Url from the cloud for the proudct main image
        /// </summary>
        /// <example>https://res.cloudinary.com/student-gordey/image/upload/v1626446506/alclnjzmwf3pqcizvfrb.jpg</example>
        public string MainImageUrl { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        /// <example>60</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Age Rating of the product
        /// </summary>
        /// <example>6</example>
        public int AgeRating { get; set; }

        /// <summary>
        /// Count of the product
        /// </summary>
        /// <example>10</example>
        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
