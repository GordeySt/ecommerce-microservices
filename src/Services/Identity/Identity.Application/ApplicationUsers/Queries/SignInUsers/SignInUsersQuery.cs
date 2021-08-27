using Identity.Application.Common;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.SignInUsers
{
    public record SignInUsersQuery : IRequest<ServiceResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class SignInUserQueryHandler : IRequestHandler<SignInUsersQuery, ServiceResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignInUserQueryHandler(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ServiceResult> Handle(SignInUsersQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);

            if (user is null)
            {
                return new ServiceResult(ServiceResultType.Unauthorized,
                    UnauthorizedExceptionMessageConstants.InvalidEmailMessage);
            }

            if (!user.EmailConfirmed)
            {
                return new ServiceResult(ServiceResultType.Unauthorized,
                    UnauthorizedExceptionMessageConstants.EmailNotConfirmedMessage);
            }

            var result = await _signInManager.PasswordSignInAsync(user, query.Password, false, false);

            if (!result.Succeeded)
            {
                return new ServiceResult(ServiceResultType.Unauthorized, 
                    UnauthorizedExceptionMessageConstants.InvalidPasswordMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
