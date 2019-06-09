using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private const string EmailAdress = "development.email.server123@gmail.com";
        private const string Password = "obichammitov";
        private const string Username = "Team Collab";

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var toAddress = new MailAddress(email);
            var fromAddress = new MailAddress(EmailAdress, Username);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, Password),
                Timeout = 20000
            };

            using (var mailMessage = new MailMessage(fromAddress, toAddress))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
