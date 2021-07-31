using Identity.Application.Common;
using Identity.Application.Common.Utilities;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationRoles.Commands.GrantRoleToUser
{
    public record GrantRoleToUserCommand(Guid RoleId, Guid UserId) : IRequest<ServiceResult>;

    public class GrantRoleToUserCommandHandler 
        : IRequestHandler<GrantRoleToUserCommand, ServiceResult>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GrantRoleToUserCommandHandler(RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(GrantRoleToUserCommand request,
            CancellationToken cancellationToken)
        {
            var appRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (appRole is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundRoleMessage);
            }

            var appUser = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (appUser is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundUserMessage);
            }

            var isUserInRole = await _userManager.IsInRoleAsync(appUser, appRole.Name);

            if (isUserInRole)
            {
                return new ServiceResult(ServiceResultType.BadRequest,
                    BadRequestExceptionMessageConstants.UserIsInRoleMessage);
            }

            var roleGrantResult = await _userManager.AddToRoleAsync(appUser, appRole.Name);

            if (!roleGrantResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(roleGrantResult.Errors));
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
        
}
