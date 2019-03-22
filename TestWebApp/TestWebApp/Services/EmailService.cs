using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestWebApp.Services
{
    public class EmailService
    {

        private readonly IConfiguration _config;

        private string serverHost;
        private int serverPort;
        private bool useSsl;
        private string authentificationEmailLogin;
        private string authentificationEmailPassword;
        private string senderName;

        public EmailService(IConfiguration config)
        {
            _config = config;
            serverHost = _config["SMTPSettings:ServerHost"];
            serverPort = Convert.ToInt32(_config["SMTPSettings:ServerPort"]);
            useSsl = Convert.ToBoolean(_config["SMTPSettings:IsSsl"]);
            authentificationEmailLogin = _config["SMTPSettings:AuthentificationEmailLogin"];
            authentificationEmailPassword = _config["SMTPSettings:AuthentificationEmailPassword"];
            senderName = _config["SMTPSettings:SenderName"];
        }

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
                await client.ConnectAsync(serverHost, serverPort, useSsl);
                await client.AuthenticateAsync(authentificationEmailLogin, authentificationEmailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }


    }
}
