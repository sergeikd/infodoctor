using Infodoctor.Web.Infrastructure.Interfaces;
using System.Configuration;

namespace Infodoctor.Web.Infrastructure
{
    public class ConfigService// : IConfigService
    {
        public string PathToArticlesImages => ConfigurationManager.AppSettings["PathToArticlesImages"];
        public string PathToClinicsImages => ConfigurationManager.AppSettings["PathToClinicsImages"];
        public string PathToResortsImages => ConfigurationManager.AppSettings["PathToResortsImages"];
        public string PathToDoctorsImages => ConfigurationManager.AppSettings["PathToDoctorsImages"];
        public string SmtpServer => ConfigurationManager.AppSettings["SmtpServer"];
        public string SmtpPort => ConfigurationManager.AppSettings["SmtpPort"];
        public string Email => ConfigurationManager.AppSettings["Email"];
        public string Password => ConfigurationManager.AppSettings["Password"];
    }
}