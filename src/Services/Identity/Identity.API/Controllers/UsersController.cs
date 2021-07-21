using Identity.Application.ApplicationUsers.Commands.DeleteUsers;
using Identity.Application.ApplicationUsers.DTOs;
using Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Constants;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        [HttpGet("currentUser")]
        public async Task<ActionResult<ApplicationUserDto>> GetCurrentUser()
        {
            var currentUserResult = await Mediator.Send(new GetCurrentUserQuery());

            if (currentUserResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)currentUserResult.Result, currentUserResult.Message);
            }

            return currentUserResult.Data;
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = ApplicationRolesConstants.AdministratorRole)]
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
