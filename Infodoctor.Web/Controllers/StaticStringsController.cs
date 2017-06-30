using System;
using System.Linq;
using System.Web.Http;
using Infodoctor.Web.Infrastructure;
using Newtonsoft.Json;

namespace Infodoctor.Web.Controllers
{
    public class StaticStringsController : ApiController
    {
        private readonly ConfigService _configService;
        public StaticStringsController(ConfigService configService)
        {
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));
            _configService = configService;
        }

        // GET: api/{lang}/StaticStrings
        [AllowAnonymous]
        [HttpGet]
        [Route("api/{lang}/StaticStrings")]
        public string Get(string lang)
        {
            var language = StaticStrings.LanguageNames.Contains(lang) ? lang : _configService.DefaultLangCode;
            var index = Array.IndexOf(StaticStrings.LanguageNames, language);
            var result = StaticStrings.StringValues.ToDictionary(line => line[0], line => line[index + 1]);
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;
        }
    }
}
