using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Application.Common.Utilities;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Constants;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Commands.SignupUsers
{
    public class SignupUserCommand : IRequest<ServiceResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Origin { get; set; }
    }

    public class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, ServiceResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public SignupUserCommandHandler(UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ServiceResult> Handle(SignupUserCommand request, 
            CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not null)
            {
                return new ServiceResult(ServiceResultType.BadRequest,
                    ExceptionMessageConstants.ExistedEmailMessage);
            }

            var user = CreateNewApplicationUser(request);

            var userCreationResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreationResult.Succeeded)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    DatabaseUtilities.CreateErrorMessage(userCreationResult.Errors));
            }

            await _userManager.AddToRoleAsync(user, ApplicationRolesConstants.UserRole);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailVerificationAsync(token, request.Origin, request.Email);

            return new ServiceResult(ServiceResultType.Success);
        }

        private ApplicationUser CreateNewApplicationUser(SignupUserCommand request) => new()
        {
            Email = request.Email,
            UserName = request.Email
        };
    }
}
