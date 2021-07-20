using Identity.Application.ApplicationUsers.Commands.ConfirmEmails;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Application.ApplicationUsers.Queries.ResendEmailVerifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignupUser(SignupUserCommand command)
        {
            command.Origin = Request.Headers["origin"];

            var signUpResult = await Mediator.Send(command);

            if (signUpResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)signUpResult.Result, signUpResult.Message);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(ConfirmEmailCommand command)
        {
            var verificationEmailResult = await Mediator.Send(command);

            if (verificationEmailResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)verificationEmailResult.Result,
                    verificationEmailResult.Message);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("resendEmailVerification")]
        public async Task<IActionResult> ResendEmailVerification([FromQuery] ResendEmailVerificationQuery query)
        {
            query.Origin = Request.Headers["origin"];

            await Mediator.Send(query);

            return NoContent();
        }
    }
}
