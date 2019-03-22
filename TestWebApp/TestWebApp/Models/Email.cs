using System;

namespace TestWebApp.Models
{
    /// <summary>
    /// Класс-модель эмейл сообщения
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Идентификатор эмейла, первичный ключ в БД
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тема эмейла
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Тело эмейла
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Получатели сообщения
        /// </summary>
        public string[] Recipients { get; set; }
        /// <summary>
        /// Дата создания = дате обработки POST-запроса к Web сервису
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Результат отправки эмейла (Ok/Failed)
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Текст ошибки, в случае её возникновения
        /// </summary>
        public string FailedMessage { get; set; }
    }
}