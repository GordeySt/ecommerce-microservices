using NetEscapades.Configuration.Validation;

namespace OcelotApiGateway.Settings
{
    public class AppSettings : IValidatable
    {
        public AppUrlsSettings AppUrlsSettings { get; set; }

        public void Validate()
        {
            AppUrlsSettings.Validate();
        }
    }
}
