using Microsoft.EntityFrameworkCore;

namespace TestWebApp.Models
{
    public class MailContext : DbContext
    {
        public MailContext(DbContextOptions<MailContext> options) : base(options) { }
        
        public DbSet<Email> Emails { get; set; }
    }
}