using Identity.Application.Common;
using Identity.Application.Common.Utilities;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationRoles.Commands.CreateRoles
{
    public class CreateRoleCommand : IRequest<ServiceResult<ApplicationRole>>
    {
        public string RoleName { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ServiceResult<ApplicationRole>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceResult<ApplicationRole>> Handle(CreateRoleCommand request,
            CancellationToken cancellationToken)
        {
            var isRoleExisted = await _roleManager.RoleExistsAsync(request.RoleName);

            if (isRoleExisted)
            {
                return new ServiceResult<ApplicationRole>(ServiceResultType.BadRequest, 
                    BadRequestExceptionMessageConstants.RoleAlreadyExistsMessage);
            }

            var newRole = new ApplicationRole { Name = request.RoleName };

            var roleCreationResult = await _roleManager.CreateAsync(newRole);

            if (!roleCreationResult.Succeeded)
            {
                return new ServiceResult<ApplicationRole>(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(roleCreationResult.Errors));
            }

            return new ServiceResult<ApplicationRole>(ServiceResultType.Success, newRole);
        }
    }
}
