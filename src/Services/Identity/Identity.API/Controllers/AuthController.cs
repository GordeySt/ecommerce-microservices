using Identity.Application.ApplicationUsers.Commands.ConfirmEmails;
using Identity.Application.ApplicationUsers.Commands.ResetPasswords;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Application.ApplicationUsers.Queries.ResendEmailVerifications;
using Identity.Application.ApplicationUsers.Queries.SendResetPasswordEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        /// <summary>
        /// SignUp Users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Auth/signup
        ///     { 
        ///         "email": "snu23535@eoopy.com",
        ///         "password": "password1",
        ///         "origin": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">SignUp Data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If Email already exists</response>
        /// <response code="500">If there are problems with creating user</response>
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Verify User Email Address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Auth/verifyEmail
        ///     { 
        ///         "token": "CfDJ8HhXpBD38pxKlQ59bbubbjVl7Aiv+mI3Nd9OMXO9DGC+BXXy
        ///         wUJVNKodWrn6M+a3LC65BILD2SGzVahVxNy5AO7eCQyzXxUPy1Ym
        ///         U0iqSCI7QKKHbordfy1vUh2B0venGjH0WRdWlknWWPa7oE0krqq7v
        ///         IPAn428aub21/2qCehtim9gQAnVMRdu5WSzD/vqClnjWT3XN68+/uFaoTgeLXO3iVaHI2H4QUq0NP/IPWeu",
        ///         "email": "snu23535@eoopy.com"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">Email Confirmation data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If there are problems verifying email</response>
        [HttpPost("verifyemail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Resend Email Verification to activate account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Auth/resendEmailVerification
        ///     { 
        ///         "email": "snu23535@eoopy.com",
        ///         "origin": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="query">Email Confirmation data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If user with this email not found</response>
        [HttpGet("resendemailverification")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResendEmailVerification([FromQuery] ResendEmailVerificationQuery query)
        {
            query.Origin = Request.Headers["origin"];

            var resendEmailVerificationResult = await Mediator.Send(query);

            if (resendEmailVerificationResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)resendEmailVerificationResult.Result, 
                    resendEmailVerificationResult.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Send email for reseting password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Auth/sendResetPasswordEmail
        ///     { 
        ///         "email": "snu23535@eoopy.com",
        ///         "origin": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="query">Email for reseting password data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="404">If user with this email not found</response>
        [HttpGet("sendresetpasswordemail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Reset password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Auth/resetPassword
        ///     { 
        ///         "email": "snu23535@eoopy.com",
        ///         "token": "CfDJ8HhXpBD38pxKlQ59bbubbjVl7Aiv+mI3Nd9OMXO9DGC+BXXywUJVN
        ///         KodWrn6M+a3LC65BILD2SGzVahVxNy5AO7eCQyzXxUPy1YmU0iqSCI7QKKHbordfy1v
        ///         Uh2B0venGjH0WRdWlknWWPa7oE0krqq7vIPAn428aub21/2qCehtim9gQAnVMRdu5WS
        ///         zD/vqClnjWT3XN68+/uFaoTgeLXO3iVaHI2H4QUq0NP/IPWeu",
        ///         "password": "password2",
        ///         "confirmPassword": "password2"
        ///     }
        /// 
        /// </remarks>
        /// <param name="command">Reset password data</param>
        /// <returns>Returns NoContent object result</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If there are problems reseting password</response>
        /// <response code="404">If user with this email not found</response>
        [HttpPost("resetpassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
