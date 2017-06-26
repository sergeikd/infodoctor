namespace Infodoctor.BL.DtoModels
{
    public class DtoMailMessage
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class DtoMailServiceConfiguration
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }
}
