using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Application.ApplicationUsers.DTOs;
using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo
{
    public class GetCurrentUserQuery : IRequest<ServiceResult<ApplicationUserDto>>
    {
    }

    public class GetUserByTokenInfoQueryHandler : IRequestHandler<GetCurrentUserQuery,
        ServiceResult<ApplicationUserDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByTokenInfoQueryHandler(ICurrentUserService currentUserService, 
            IApplicationDbContext dbContext, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ApplicationUserDto>> Handle(GetCurrentUserQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id.ToString() == _currentUserService.UserId, 
                     cancellationToken);

            if (user is null)
            {
                return new ServiceResult<ApplicationUserDto>(ServiceResultType.BadRequest,
                    ExceptionMessageConstants.InvalidTokenMessage);
            }

            return new ServiceResult<ApplicationUserDto>(ServiceResultType.Success, user);
        }
    }
}
