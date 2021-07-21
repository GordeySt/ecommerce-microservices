using Identity.Application.ApplicationUsers.Commands.ConfirmEmails;
using Identity.Application.ApplicationUsers.Commands.ResetPasswords;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Application.ApplicationUsers.Queries.ResendEmailVerifications;
using Identity.Application.ApplicationUsers.Queries.SendResetPasswordEmail;
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

        [AllowAnonymous]
        [HttpGet("sendResetPasswordEmail")]
        public async Task<IActionResult> SendResetPasswordEmail([FromQuery] SendResetPasswordEmailQuery query)
        {
            query.Origin = Request.Headers["origin"];

            var sendEmailResult = await Mediator.Send(query);

            if (sendEmailResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)sendEmailResult.Result, sendEmailResult.Message);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var resetPasswordResult = await Mediator.Send(command);

            if (resetPasswordResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)resetPasswordResult.Result, resetPasswordResult.Message);
            }

            return NoContent();
        }
    }
}
