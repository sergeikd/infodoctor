using Infodoctor.BL.Services;

namespace Infodoctor.BL.Interfaces
{
    public interface IYandexTranslateService
    {
        DetectLangModel DetectLang(string apiKey, string text);
    }
}
