using NetEscapades.Configuration.Validation;

namespace Catalog.API.Startup.Settings
{
    public class AppSettings : IValidatable
    {
        public DbSettings DbSettings { get; set; }
        public CloudinarySettings CloudinarySettings { get; set; }

        public void Validate()
        {
            DbSettings.Validate();
            CloudinarySettings.Validate();
        }
    }
}
