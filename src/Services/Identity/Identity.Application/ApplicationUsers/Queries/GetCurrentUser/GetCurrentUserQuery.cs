using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Application.ApplicationUsers.DTOs;
using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo
{
    public record GetCurrentUserQuery : IRequest<ServiceResult<ApplicationUserDto>>;

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery,
        ServiceResult<ApplicationUserDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ApplicationUserDto>> Handle(GetCurrentUserQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(x => x.AppUserRoles)
                .ThenInclude(x => x.AppRole)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.ToString() == _currentUserService.UserId,
                     cancellationToken);

            if (user is null)
            {
                return new ServiceResult<ApplicationUserDto>(ServiceResultType.BadRequest,
                    ExceptionMessageConstants.InvalidTokenMessage);
            }

            var userDto = _mapper.Map<ApplicationUserDto>(user);

            return new ServiceResult<ApplicationUserDto>(ServiceResultType.Success, userDto);
        }
    }
}
