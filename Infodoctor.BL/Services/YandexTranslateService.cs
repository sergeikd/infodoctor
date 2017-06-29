using System;
using System.Net;
using Infodoctor.BL.Interfaces;
using Newtonsoft.Json;

namespace Infodoctor.BL.Services
{
    public class YandexTranslateService : IYandexTranslateService
    {
        public DetectLangModel DetectLang(string apiKey, string text)
        {
            var url = string.Format("https://translate.yandex.net/api/v1.5/tr.json/detect?text={1}&key={0}", apiKey, text);

            var json = DownloadJson(url);
            var model = DeserialiseJson(json);

            return model;
        }

        private static string DownloadJson(string url)
        {
            var client = new WebClient();
            var reply = client.DownloadString(url);
            return reply;
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
