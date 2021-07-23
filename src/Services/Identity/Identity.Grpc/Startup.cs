using Identity.Application.ApplicationRoles.Queries.GetRoles;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using Identity.Grpc.AppServices;
using Identity.Grpc.Services;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Identity.Grpc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost;Port=5420;Database=IdentityDb;User Id=admin;Password=admin1234;");
            });

            services.AddAuthentication();

            services
               .AddIdentityCore<ApplicationUser>(options =>
               {
                   options.User.RequireUniqueEmail = false;
                   options.Password.RequireDigit = true;
                   options.Password.RequireLowercase = true;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequiredUniqueChars = 0;
                   options.SignIn.RequireConfirmedEmail = true;
               })
               .AddRoles<ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddSignInManager<SignInManager<ApplicationUser>>()
               .AddDefaultTokenProviders();

            services.AddMediatR(typeof(GetRolesQuery).Assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly()) ;
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddGrpc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
