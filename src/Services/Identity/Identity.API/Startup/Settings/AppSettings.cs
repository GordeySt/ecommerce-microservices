using Identity.Infrastructure.Services.Email;
using NetEscapades.Configuration.Validation;

namespace Identity.API.Startup.Settings
{
    public class AppSettings : IValidatable
    {
        public DbSettings DbSettings { get; set; }

        public void Validate()
        {
            DbSettings.Validate();
        }
    }
}
