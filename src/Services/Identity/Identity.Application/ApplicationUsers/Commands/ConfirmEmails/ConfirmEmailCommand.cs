﻿using Identity.Application.Common;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.ApplicationUsers.Commands.ConfirmEmails
{
    public class ConfirmEmailCommand : IRequest<ServiceResult>
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }

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
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(request.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
            {
                return new ServiceResult(ServiceResultType.BadRequest, 
                    ExceptionMessageConstants.ProblemVerifyingEmailMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
