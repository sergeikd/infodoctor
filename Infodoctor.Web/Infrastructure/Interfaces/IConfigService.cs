namespace Infodoctor.Web.Infrastructure.Interfaces
{
    public interface IConfigService
    {
        string PathToArticlesImages { get; }
        string PathToDoctorsImages { get; }
        string PathToClinicsImages { get; }
        string SmtpServer { get; }
        string SmtpPort { get; }
        string Email { get; }
        string Password { get; }
    }
}
