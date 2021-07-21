using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Application.ApplicationRoles.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationRoles.Queries.GetRoles
{
    public record GetRolesQuery : IRequest<List<ApplicationRoleDto>>;

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<ApplicationRoleDto>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<ApplicationRoleDto>> Handle(GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            return await _roleManager.Roles
                .AsNoTracking()
                .ProjectTo<ApplicationRoleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
