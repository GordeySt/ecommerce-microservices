using Identity.Application.ApplicationUsers.Commands.DeleteUsers;
using Identity.Application.ApplicationUsers.DTOs;
using Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<ActionResult<ApplicationUserDto>> GetUserByTokenInfo()
        {
            var result = await Mediator.Send(new GetCurrentUserQuery());

            if (result.Result is ServiceResultType.BadRequest)
            {
                return BadRequest(result.Message);
            }

            return result.Data;
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await Mediator.Send(new DeleteUserCommand { Id = id });

            if (result.Result is ServiceResultType.NotFound)
            {
                return NotFound(result.Message);
            }

            if (result.Result is ServiceResultType.BadRequest)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
