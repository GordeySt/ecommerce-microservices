using Identity.Application.ApplicationUsers.Queries.LogoutUsers;
using Identity.Application.ApplicationUsers.Queries.SignInUsers;
using Identity.Domain.Entities;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    public class AuthMvcController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly ISender _mediator;

        public AuthMvcController(IIdentityServerInteractionService interactionService, ISender mediator)
        {
            _interactionService = interactionService;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new SignInUsersQuery
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInUsersQuery query)
        {
            if (!ModelState.IsValid)
            {
                return View(query);
            }

            var result = await _mediator.Send(query);

            if (result.Result is not ServiceResultType.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(query);
            }

            return Redirect(query.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(LogoutUserQuery query)
        {
            await _mediator.Send(query);

            var logoutRequest = await _interactionService.GetLogoutContextAsync(query.LogoutId);
            
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
