using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestWebApp.Models;
using TestWebApp.Services;

namespace TestWebApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/mails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private IConfiguration _configuration;
        EmailService emailService;

        public EmailsController(IConfiguration Configuration, MailContext context)
        {
            _configuration = Configuration;
            _context = context;
            emailService = new EmailService(_configuration);
        }

        private readonly MailContext _context;

        //public EmailsController(MailContext context)
        //{
        //    _context = context;
        //}

        // GET api/mails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetAllEmails()
        {
            return await _context.Emails.ToListAsync();
        }

        // POST api/mails
        [HttpPost]
        public async Task<IActionResult> PostEmailItemAsync(Email email)
        {
            Random rnd = new Random();
            email.Id = rnd.Next();
            email.CreateTime = DateTime.Now;
            try
            {
                await emailService.SendEmailAsync(email.Recipients, email.Subject, email.Body);
                email.Result = "Ok";
            }
            catch (Exception e)
            {
                email.Result = "Failed";
                email.FailedMessage = e.ToString();
            }
            _context.Emails.Add(email);
            _context.SaveChanges();

            return Ok(email);
        }
    }
}
