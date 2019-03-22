using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestWebApp.Models;
using TestWebApp.Services;

namespace TestWebApp.Controllers
{

    /// <summary>
    /// Контроллер для обработки HTTP запросов
    /// </summary>
    [Route("api/mails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly MailContext _context;
        EmailService emailService;

        /// <summary>
        /// Конструктор для заполнения конфигурации и данных БД
        /// </summary>
        /// <param name="Configuration">Конфигурация</param>
        /// <param name="context">Данные БД</param>
        public EmailsController(IConfiguration Configuration, MailContext context)
        {
            _configuration = Configuration;
            _context = context;
            emailService = new EmailService(_configuration);
        }

        /// <summary>
        /// Метод обработки GET запроса. 
        /// </summary>
        /// <returns>Все записи об эмейлах из БД</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetAllEmails()
        {
            return await _context.Emails.ToListAsync();
        }

        /// <summary>
        /// Метод обработчик POST запроса. 
        /// Формирует email сообщение и выполняет его отправку.
        /// Добавляет запись в БД.
        /// Заполняет дату создания и результат отправки в виде поля Result: (значения Ok, Failed), а также поле FailedMessage(текст ошибки, либо null)
        /// </summary>
        /// <param name="email">Эмейл сформированный в POST запросе</param>
        /// <returns>Результат задачи по отправке эмейла (эмейл дополненный новыми полями)</returns>
        [HttpPost]
        public async Task<IActionResult> PostEmailItemAsync(Email email)
        {
            Random rnd = new Random();
            email.Id = rnd.Next();
            email.CreateTime = DateTime.Now;
            try
            {
                _context.Emails.Add(email);
                _context.SaveChanges();
                await emailService.SendEmailAsync(email.Recipients, email.Subject, email.Body);
                email.Result = "Ok";
            }
            catch (Exception e)
            {
                email.Result = "Failed";
                email.FailedMessage = e.ToString();
                Console.WriteLine(e.ToString());
            }
            return Ok(email);
        }
    }
}
