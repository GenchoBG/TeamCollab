using System.Threading.Tasks;

namespace TeamCollab.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
