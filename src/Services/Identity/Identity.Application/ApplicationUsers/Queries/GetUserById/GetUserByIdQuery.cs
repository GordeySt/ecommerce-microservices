using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ServiceResult<ApplicationUser>>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ServiceResult<ApplicationUser>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUserByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResult<ApplicationUser>> Handle(GetUserByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                return new ServiceResult<ApplicationUser>(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundUserMessage);
            }

            return new ServiceResult<ApplicationUser>(ServiceResultType.Success, user);
        }
    }
}
