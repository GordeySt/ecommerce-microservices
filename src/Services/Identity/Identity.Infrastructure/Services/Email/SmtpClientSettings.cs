namespace Identity.Infrastructure.Services.Email
{
    public class SmtpClientSettings
    {
        public string Host { get; set; }
        public string SenderEmailLogin { get; set; }
        public string SenderEmailPassword { get; set; }
    }
}
