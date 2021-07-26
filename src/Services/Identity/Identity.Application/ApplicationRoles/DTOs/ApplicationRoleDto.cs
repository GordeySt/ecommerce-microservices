using AutoMapper;
using Identity.Application.Common.Mappings;
using Identity.Domain.Entities;
using System;

namespace Identity.Application.ApplicationRoles.DTOs
{
    public class ApplicationRoleDto : IMapFrom<ApplicationUserRole>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationRole, ApplicationRoleDto>();
            profile.CreateMap<ApplicationUserRole, ApplicationRoleDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.AppRole.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.AppRole.Name));
        }
    }
}
