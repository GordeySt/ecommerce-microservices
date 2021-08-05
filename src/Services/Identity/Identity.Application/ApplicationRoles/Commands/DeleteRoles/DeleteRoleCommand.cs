using Identity.Application.Common.Utilities;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationRoles.Commands.DeleteRoles
{
    public record DeleteRoleCommand(Guid Id) : IRequest<ServiceResult>;

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ServiceResult>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceResult> Handle(DeleteRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());

            if (role is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    ExceptionConstants.NotFoundItemMessage);
            }

            var deleteUserResult = await _roleManager.DeleteAsync(role);

            if (!deleteUserResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(deleteUserResult.Errors));
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
