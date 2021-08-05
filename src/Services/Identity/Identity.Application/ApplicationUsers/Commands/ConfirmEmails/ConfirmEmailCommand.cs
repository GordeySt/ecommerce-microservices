using Identity.Application.Common;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Commands.ConfirmEmails
{
    public record ConfirmEmailCommand(string Token, string Email) : IRequest<ServiceResult>;

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ServiceResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundUserMessage);
            }

            var confirmationResult = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!confirmationResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.BadRequest, 
                    BadRequestExceptionMessageConstants.ProblemVerifyingEmailMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
