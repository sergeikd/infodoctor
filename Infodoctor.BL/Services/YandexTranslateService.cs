using System;
using System.Net;
using System.Threading.Tasks;
using Infodoctor.BL.Interfaces;
using Newtonsoft.Json;

namespace Infodoctor.BL.Services
{
    public class YandexTranslateService : IYandexTranslateService
    {
        public DetectLangModel DetectLang(string apiKey, string text)
        {
            var url = string.Format("https://translate.yandex.net/api/v1.5/tr.json/detect?text={1}&key={0}", apiKey, text);

            string json;

            var task = Task.Run(() => DownloadJson(url));
            if (task.Wait(TimeSpan.FromSeconds(5)))
                json = task.Result;
            else
                return null;

            var model = DeserialiseJson(json);

            return model;
        }

        private static string DownloadJson(string url)
        {
            try
            {
                var client = new WebClient();
                var reply = client.DownloadString(url);
                return reply;
            }
            catch (Exception)
            {
                return string.Empty;
            }   
        }

        private static DetectLangModel DeserialiseJson(string json)
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));
            try
            {
                var langModel = JsonConvert.DeserializeObject<DetectLangModel>(json);
                return langModel;
            }
            catch
            {
                return null;
            }
        }
    }


    #region Models
    public class DetectLangModel
    {
        public int Code { get; set; }
        public string Lang { get; set; }
    }
    #endregion
}
