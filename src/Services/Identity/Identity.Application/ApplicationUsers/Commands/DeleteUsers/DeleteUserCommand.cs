using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Commands.DeleteUsers
{
    public class DeleteUserCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ServiceResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteUserCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResult> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionMessageConstants.NotFoundItemMessage);
            }

            _dbContext.Remove(user);

            var isSuccess = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            if (!isSuccess)
            {
                return new ServiceResult(ServiceResultType.BadRequest,
                    ExceptionMessageConstants.ProblemDeletingItemMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
