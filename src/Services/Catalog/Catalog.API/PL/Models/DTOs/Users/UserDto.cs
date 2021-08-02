using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs.ProductRatings;
using System;
using System.Collections.Generic;

namespace Catalog.API.PL.Models.DTOs.Users
{
    public class UserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public ICollection<ProductRatingDto> Ratings { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                 .ForMember(d => d.Ratings, options => options.MapFrom(source => source.Ratings));
        }
    }
}
