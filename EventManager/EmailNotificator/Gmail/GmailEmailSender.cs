using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EmailNotificator.Gmail
{
    public class GmailEmailSender
    {
        private readonly string email;
        private readonly string emailPassword;

        public GmailEmailSender()
        {
            emailPassword = Environment.GetEnvironmentVariable("EmailPassword");
            email = Environment.GetEnvironmentVariable("Email");
        }

        public async Task SendEmailAsync(string to, string subject, string body, string from)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Система управления мероприятиями Eventoto", email));
            message.To.Add(new MailboxAddress("Получатель", to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                // здесь вместо обычного пароля используем пароль приложения
                client.Authenticate(email, emailPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
