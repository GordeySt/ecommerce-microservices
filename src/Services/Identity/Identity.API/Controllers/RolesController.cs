using Identity.Application.ApplicationRoles.Commands.CreateRoles;
using Identity.Application.ApplicationRoles.Commands.DeleteRoles;
using Identity.Application.ApplicationRoles.Commands.GrantRoleToUser;
using Identity.Application.ApplicationRoles.Commands.RevokeRoleFromUser;
using Identity.Application.ApplicationRoles.Commands.UpdateRoles;
using Identity.Application.ApplicationRoles.DTOs;
using Identity.Application.ApplicationRoles.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Constants;
using Services.Common.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = ApplicationRolesConstants.AdministratorRole)]
    public class RolesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ApplicationRoleDto>>> GetRoles() => 
            await Mediator.Send(new GetRolesQuery());

        [HttpPost("grant")]
        public async Task<IActionResult> GrantRoleToUser(GrantRoleToUserCommand command)
        {
            var roleGrantResult = await Mediator.Send(command);

            if (roleGrantResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)roleGrantResult.Result, roleGrantResult.Message);
            }

            return NoContent();
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRoleFromUser(RevokeRoleFromUserCommand command)
        {
            var revokeRoleFromUserResult = await Mediator.Send(command);

            if (revokeRoleFromUserResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)revokeRoleFromUserResult.Result,
                    revokeRoleFromUserResult.Message);
            }

            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            var roleCreationResult = await Mediator.Send(command);

            if (roleCreationResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)roleCreationResult.Result, 
                    roleCreationResult.Message);
            }

            return CreatedAtAction(nameof(CreateRole), roleCreationResult.Data);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var deleteRoleResult = await Mediator.Send(new DeleteRoleCommand { Id = id });

            if (deleteRoleResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)deleteRoleResult.Result, deleteRoleResult.Message);
            }

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRole(Guid id, UpdateRoleCommand command)
        {
            command.RoleId = id;

            var updateRoleResult = await Mediator.Send(command);

            if (updateRoleResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)updateRoleResult.Result, updateRoleResult.Message);
            }

            return NoContent();
        }
    }
}
