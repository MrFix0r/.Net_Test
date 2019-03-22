using System;

namespace TestWebApp.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Recipients { get; set; }
        public DateTime CreateTime { get; set; }
        public string Result { get; set; }
        public string FailedMessage { get; set; }
    }
}