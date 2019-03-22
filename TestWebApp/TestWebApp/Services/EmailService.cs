using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using TestWebApp.Models;

namespace TestWebApp.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string[] recipients, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Тестовое задание Роспартнер","mrfix0r@gmail.com"));
            foreach (string recipient in recipients)
            {
                emailMessage.To.Add(new MailboxAddress("", recipient));
            }
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                //TODO вынести в отдельный файл конфигурации
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("mrfix0r@gmail.com", "qarynelffgoqaiip");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }


    }
}
