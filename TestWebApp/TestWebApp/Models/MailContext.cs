using Microsoft.EntityFrameworkCore;

namespace TestWebApp.Models
{
    /// <summary>
    /// Класс контекст данных для взаимодействия с БД по средствам Entity Framework
    /// </summary>
    public class MailContext : DbContext
    {
        /// <summary>
        /// Конструктор заполняющий настройки БД (TODO задать вопрос)
        /// </summary>
        /// <param name="options">Настройки</param>
        public MailContext(DbContextOptions<MailContext> options) : base(options) { }
        
        /// <summary>
        /// Метод для работы с моделью Email
        /// </summary>
        public DbSet<Email> Emails { get; set; }
    }
}