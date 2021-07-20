using AutoMapper;
using Identity.Application.Common.Mappings;
using Identity.Domain.Entities;
using System;

namespace Identity.Application.ApplicationUsers.DTOs
{
    public class ApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, ApplicationUserDto>();
        }
    }
}
