﻿using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestWebApp.Services
{

    /// <summary>
    /// Сервис отправки писем по протоколу SMTP
    /// </summary>
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly string serverHost;
        private readonly int serverPort;
        private readonly bool useSsl;
        private readonly string authentificationEmailLogin;
        private readonly string authentificationEmailPassword;

        /// <summary>
        /// Конструктор для получения конфигурации SMTP сервера
        /// </summary>
        /// <param name="config">Конфигурация</param>
        public EmailService(IConfiguration config)
        {
            _config = config;
            try
            {
                serverHost = _config["SMTPSettings:ServerHost"];
                serverPort = Convert.ToInt32(_config["SMTPSettings:ServerPort"]);
                useSsl = Convert.ToBoolean(_config["SMTPSettings:IsSsl"]);
                authentificationEmailLogin = _config["SMTPSettings:AuthentificationEmailLogin"];
                authentificationEmailPassword = _config["SMTPSettings:AuthentificationEmailPassword"];
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Метод отправки email сообщения по средствам SMTP-клиента.
        /// Конфигурация SMTP-сервера задаётся в appsettings.json
        /// Email сообщение формируется по средствам внешней Open Source библиотеки MailKit
        /// </summary>
        /// <param name="recipients">Получатели</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="message">Содержание письма</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string[] recipients, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Тестовое задание", authentificationEmailLogin));
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
