using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class AutoMapperExtensions
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SignupUserCommand).Assembly);
        }
    }
}
