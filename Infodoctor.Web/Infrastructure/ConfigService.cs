using System.Configuration;
using System.Drawing;

namespace Infodoctor.Web.Infrastructure
{
    public class ConfigService// : IConfigService
    {
        public string PathToArticlesImages => ConfigurationManager.AppSettings["PathToArticlesImages"];
        public string PathToClinicsImages => ConfigurationManager.AppSettings["PathToClinicsImages"];
        public string PathToResortsImages => ConfigurationManager.AppSettings["PathToResortsImages"];
        public string PathToDoctorsImages => ConfigurationManager.AppSettings["PathToDoctorsImages"];
        public string PathToClinicSourceImages => ConfigurationManager.AppSettings["PathToClinicSourceImages"];
        public string PathToOldDbClinics => ConfigurationManager.AppSettings["PathToOldDbClinics"];
        public string DefaultLangCode => ConfigurationManager.AppSettings["DefaultLangCode"];
        public string YandexTranslateApiKey => ConfigurationManager.AppSettings["YandexTranslateApiKey"];
        #region first attempt to organize images sizes config
        //public int ImageWidthLarge => int.Parse(ConfigurationManager.AppSettings["ImageWidthLarge"]);
        //public int ImageHeightLarge => int.Parse(ConfigurationManager.AppSettings["ImageHeightLarge"]);
        //public int ImageWidthMedium => int.Parse(ConfigurationManager.AppSettings["ImageWidthMedium"]);
        //public int ImageHeightMedium => int.Parse(ConfigurationManager.AppSettings["ImageHeightMedium"]);
        //public int ImageWidthSmall => int.Parse(ConfigurationManager.AppSettings["ImageWidthSmall"]);
        //public int ImageHeightSmall => int.Parse(ConfigurationManager.AppSettings["ImageHeightSmall"]);
        #endregion
        //we use Point class just for store required images sizes
        public Point[] ImagesSizes = {
            new Point { X = int.Parse(ConfigurationManager.AppSettings["ImageWidthLarge"]), Y = int.Parse(ConfigurationManager.AppSettings["ImageHeightLarge"]) },
            new Point { X = int.Parse(ConfigurationManager.AppSettings["ImageWidthMedium"]), Y = int.Parse(ConfigurationManager.AppSettings["ImageHeightMedium"])},
            new Point { X = int.Parse(ConfigurationManager.AppSettings["ImageWidthSmall"]), Y = int.Parse(ConfigurationManager.AppSettings["ImageHeightSmall"]) }};
        public string SmtpServer => ConfigurationManager.AppSettings["SmtpServer"];
        public string SmtpPort => ConfigurationManager.AppSettings["SmtpPort"];
        public string Email => ConfigurationManager.AppSettings["Email"];
        public string Password => ConfigurationManager.AppSettings["Password"];
    }
}