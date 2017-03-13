using Infodoctor.Web.Infrastructure.Interfaces;
using System.Configuration;

namespace Infodoctor.Web.Infrastructure
{
    public class ConfigService : IConfigService
    {
        public string PathToArticlesImages => ConfigurationManager.AppSettings["PathToArticlesImages"];
        public string PathToClinicsImages => ConfigurationManager.AppSettings["PathToClinicsImages"];
        public string PathToDoctorsImages => ConfigurationManager.AppSettings["PathToDoctorsImages"];
        public string ServerDomain => ConfigurationManager.AppSettings["ServerDomain"];
    }
}