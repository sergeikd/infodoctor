using Infodoctor.Web.Infrastructure.Interfaces;
using System.Configuration;

namespace Infodoctor.Web.Infrastructure
{
    public class ConfigService : IConfigService
    {
        public string PathToArticleImages => ConfigurationManager.AppSettings["PathToArticleImages"];
    }
}