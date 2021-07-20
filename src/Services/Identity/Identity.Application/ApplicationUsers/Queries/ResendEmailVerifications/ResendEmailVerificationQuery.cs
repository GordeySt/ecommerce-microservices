using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.ResendEmailVerifications
{
    public class ResendEmailVerificationQuery : IRequest
    {
        public string Email { get; set; }
        public string Origin { get; set; }
    }

    public class ResendEmailVerificationQueryHandler : IRequestHandler<ResendEmailVerificationQuery>
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IEmailService _emailService;

        public ResendEmailVerificationQueryHandler(UserManager<ApplicationUser> userManger, 
            IEmailService emailService)
        {
            _userManger = userManger;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ResendEmailVerificationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByEmailAsync(request.Email);

            var token = await _userManger.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailVerificationAsync(token, request.Origin, request.Email);

            return Unit.Value;
        }
    }
}
