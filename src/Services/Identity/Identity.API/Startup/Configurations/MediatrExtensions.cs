using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class MediatrExtensions
    {
        public static void RegisterMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(SignupUserCommand).Assembly);
        }
    }
}
