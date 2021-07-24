using Identity.Application.ApplicationRoles.Queries.GetRoles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Grpc.Startup.Configurations
{
    public static class MediatrExtensions
    {
        public static void RegisterMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetRolesQuery).Assembly);
        }
    }
}
