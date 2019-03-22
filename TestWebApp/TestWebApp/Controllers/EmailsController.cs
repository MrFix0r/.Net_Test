using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Models;
using TestWebApp.Services;

namespace TestWebApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/mails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        EmailService emailService = new EmailService();

        private readonly MailContext _context;

        // GET api/mails
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/mails
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // POST api/mails
        [HttpPost]
        public async Task<IActionResult> PostEmailItemAsync(Email email)
        {
            Random rnd = new Random();
            email.Id = rnd.Next();
            await emailService.SendEmailAsync(email.Recipients, email.Subject, email.Body);
            
            //_context.Emails.Add(email);
            //_context.SaveChanges();
            //await _context.SaveChangesAsync();

            return Ok(email);
            //CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
