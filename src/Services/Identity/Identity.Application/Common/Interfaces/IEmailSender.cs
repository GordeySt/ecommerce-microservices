using System.Threading.Tasks;

namespace Identity.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string userEmail, string emailSubject, string message);
    }
}
