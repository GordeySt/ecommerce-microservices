using Identity.Application.ApplicationRoles.Commands.CreateRoles;
using Identity.Application.ApplicationRoles.Commands.DeleteRoles;
using Identity.Application.ApplicationRoles.Commands.GrantRoleToUser;
using Identity.Application.ApplicationRoles.Commands.RevokeRoleFromUser;
using Identity.Application.ApplicationRoles.Commands.UpdateRoles;
using Identity.Application.ApplicationRoles.DTOs;
using Identity.Application.ApplicationRoles.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Constants;
using Services.Common.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Authorize(Roles = ApplicationRolesConstants.AdministratorRole)]
    public class RolesController : ApiControllerBase
    {
        /// <summary>
        /// Get List Of All Roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Roles
        /// 
        /// </remarks>
        /// <returns>List of Roles Dto object</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ApplicationRoleDto>>> GetRoles() => 
            await Mediator.Send(new GetRolesQuery());

        /// <summary>
        /// Grant Role To User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Roles/grant
        ///     { 
        ///         "RoleId": "a22a7860-084e-4980-9f82-088067074716",
        ///         "UserId": "3aac0c67-a8bf-49ca-ba38-bbc490aa9633"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">Grant Role To User Data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If user is already in that role</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        /// <response code="404">If user or role not found</response>
        [HttpPost("grant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GrantRoleToUser(GrantRoleToUserCommand command)
        {
            var roleGrantResult = await Mediator.Send(command);

            if (roleGrantResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)roleGrantResult.Result, roleGrantResult.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Revoke Role From User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Roles/revoke
        ///     { 
        ///         "RoleId": "a22a7860-084e-4980-9f82-088067074716",
        ///         "UserId": "3aac0c67-a8bf-49ca-ba38-bbc490aa9633"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">Revoke Role From User Data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If user is not in that role</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        /// <response code="404">If user or role not found</response>
        [HttpPost("revoke")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Create role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Roles/create
        ///     { 
        ///         "roleName": "Customer"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">Create Role Data</param>
        /// <returns>Returns CreatedAtAction with ApplicationRole object</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If role already exists</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Roles/a22a7860-084e-4980-9f82-088067074716
        /// 
        /// </remarks>
        /// <param name="id">Role Id (guid)</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If role not found</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        [HttpDelete("id/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var deleteRoleResult = await Mediator.Send(new DeleteRoleCommand(id));

            if (deleteRoleResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)deleteRoleResult.Result, deleteRoleResult.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Roles
        ///     { 
        ///         "id": "a22a7860-084e-4980-9f82-088067074716",
        ///         "roleName": "Administrator"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">Update Role Data</param>
        /// <returns>Returns NoContent Object Result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If role not found</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand command)
        {
            var updateRoleResult = await Mediator.Send(command);

            if (updateRoleResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)updateRoleResult.Result, updateRoleResult.Message);
            }

            return NoContent();
        }
    }
}
