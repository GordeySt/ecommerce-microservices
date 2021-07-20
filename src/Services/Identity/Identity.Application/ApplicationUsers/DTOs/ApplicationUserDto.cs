using AutoMapper;
using Identity.Application.ApplicationRoles.DTOs;
using Identity.Application.Common.Mappings;
using Identity.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Identity.Application.ApplicationUsers.DTOs
{
    public class ApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public ICollection<ApplicationRoleDto> AppUserRoles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, ApplicationUserDto>();
        }
    }
}
