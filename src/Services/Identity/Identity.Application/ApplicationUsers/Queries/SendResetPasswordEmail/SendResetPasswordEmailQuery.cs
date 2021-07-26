using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.SendResetPasswordEmail
{
    public class SendResetPasswordEmailQuery : IRequest<ServiceResult>
    {
        public string Email { get; set; }
        public string Origin { get; set; }
    }

    public class SendResetPasswordEmailQueryHandler : IRequestHandler<SendResetPasswordEmailQuery, ServiceResult>
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IEmailService _emailService;

        public SendResetPasswordEmailQueryHandler(UserManager<ApplicationUser> userManger,
            IEmailService emailService)
        {
            _userManger = userManger;
            _emailService = emailService;
        }

        public async Task<ServiceResult> Handle(SendResetPasswordEmailQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound,
                    NotFoundExceptionMessageConstants.NotFoundRoleMessage);
            }

            var token = await _userManger.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendResetPasswordEmail(token, request.Origin, request.Email);

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
