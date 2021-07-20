using System.Threading.Tasks;

namespace Identity.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string token, string origin, string email);
    }
}
