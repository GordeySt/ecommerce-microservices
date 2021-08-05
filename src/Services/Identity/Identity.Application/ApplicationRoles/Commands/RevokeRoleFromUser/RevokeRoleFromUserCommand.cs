using Identity.Application.Common;
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

namespace Identity.Application.ApplicationRoles.Commands.RevokeRoleFromUser
{
    public record RevokeRoleFromUserCommand(Guid RoleId, Guid UserId) : IRequest<ServiceResult>;

    public class RevokeRoleFromUserCommandHandler
        : IRequestHandler<RevokeRoleFromUserCommand, ServiceResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RevokeRoleFromUserCommandHandler(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResult> Handle(RevokeRoleFromUserCommand request, 
            CancellationToken cancellationToken)
        {
            var appRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (appRole is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundRoleMessage);
            }

            var appUser = await _userManager.FindByIdAsync(request.RoleId.ToString());

            if (appUser is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundUserMessage);
            }

            var isUserInRole = await _userManager.IsInRoleAsync(appUser, appRole.Name);

            if (!isUserInRole)
            {
                return new ServiceResult(ServiceResultType.BadRequest,
                    BadRequestExceptionMessageConstants.UserIsNotInRoleMessage);
            }

            var revokeRoleFromUserResult = await _userManager.RemoveFromRoleAsync(appUser, appRole.Name);

            if (!revokeRoleFromUserResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(revokeRoleFromUserResult.Errors));
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
