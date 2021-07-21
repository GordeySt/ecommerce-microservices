using Identity.Application.Common;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Commands.ResetPasswords
{
    public record ResetPasswordCommand(string Token, string Email, string Password, 
        string ConfirmPassword) : IRequest<ServiceResult>;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ServiceResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(ResetPasswordCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundUserMessage);
            }

            var resetPasswordResult = await _userManager
                .ResetPasswordAsync(user, request.Token, request.Password);

            if (!resetPasswordResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.BadRequest,
                    BadRequestExceptionMessageConstants.ProblemResetingPasswordMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
