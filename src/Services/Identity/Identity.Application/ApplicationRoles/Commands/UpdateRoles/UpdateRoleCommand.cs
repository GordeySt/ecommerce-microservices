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

namespace Identity.Application.ApplicationRoles.Commands.UpdateRoles
{
    public class UpdateRoleCommand : IRequest<ServiceResult>
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ServiceResult>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceResult> Handle(UpdateRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundRoleMessage);
            }

            role.Name = request.RoleName;

            var updateRoleResult = await _roleManager.UpdateAsync(role);

            if (!updateRoleResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(updateRoleResult.Errors));
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
