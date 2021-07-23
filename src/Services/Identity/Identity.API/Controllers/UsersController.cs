using Identity.Application.ApplicationUsers.Commands.DeleteUsers;
using Identity.Application.ApplicationUsers.DTOs;
using Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Constants;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        /// <summary>
        /// Get Current User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Users/currentUser
        /// 
        /// </remarks>
        /// <returns>Current user Dto object</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If token is invalid and it is not even possible to get user from id claim</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        [HttpGet("currentUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApplicationUserDto>> GetCurrentUser()
        {
            var currentUserResult = await Mediator.Send(new GetCurrentUserQuery());

            if (currentUserResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)currentUserResult.Result, currentUserResult.Message);
            }

            return currentUserResult.Data;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Users/a22a7860-084e-4980-9f82-088067074716
        /// 
        /// </remarks>
        /// <param name="id">User Id (guid)</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If user not found</response>
        /// <response code="401">If the user not authorized</response>
        /// <response code="403">If action is forbidden (ex: not for user role)</response>
        /// <response code="500">If there are problems deleting user</response>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = ApplicationRolesConstants.AdministratorRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleteUserResult = await Mediator.Send(new DeleteUserCommand(id));

            if (deleteUserResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)deleteUserResult.Result, deleteUserResult.Message);
            }

            return NoContent();
        }
    }
}
