using Identity.Grpc.Services;
using Identity.Grpc.Startup.Configurations;
using Identity.Grpc.Startup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.Grpc.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = ReadAppSettings(Configuration);

            services.AddControllers();

            services.AddAuthentication();

            services.RegisterDatabase(appSettings);
            services.RegisterIdentity(appSettings);
            services.ValidateSettingParameters(Configuration);

            services.RegisterServices(appSettings);

            services.RegisterAutoMapper();
            services.RegisterMediatr();

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

        private static AppSettings ReadAppSettings(IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(nameof(AppSettings.DbSettings))
                .Get<DbSettings>();

            var identitySettings = configuration.GetSection(nameof(AppSettings.IdentitySettings))
                .Get<IdentitySettings>();

            return new AppSettings
            {
                DbSettings = dbSettings,
                IdentitySettings = identitySettings
            };
        }
    }
}
