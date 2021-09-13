using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Queries.LogoutUsers
{
    public record LogoutUserQuery(string LogoutId) : IRequest<ServiceResult>;

    public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery, ServiceResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutUserQueryHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<ServiceResult> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
