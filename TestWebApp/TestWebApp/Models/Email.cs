namespace TestWebApp.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Recipients { get; set; }
    }
}