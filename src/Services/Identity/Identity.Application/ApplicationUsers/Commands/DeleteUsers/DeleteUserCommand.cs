using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Application.Common.Utilities;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundItemMessage);
            }

            var deleteUserResult = await _userManager.DeleteAsync(user);

            if (!deleteUserResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(deleteUserResult.Errors));
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
