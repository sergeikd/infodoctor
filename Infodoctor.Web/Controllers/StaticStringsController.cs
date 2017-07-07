using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Infodoctor.Web.Infrastructure;

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
        public Dictionary<string, string> Get(string lang)
        {
            var language = StaticStrings.LanguageNames.Contains(lang) ? lang : _configService.DefaultLangCode;
            var index = Array.IndexOf(StaticStrings.LanguageNames, language);
            var result = StaticStrings.StringValues.ToDictionary(line => line[0], line => line[index + 1]);
            //var json = JsonConvert.SerializeObject(result, Formatting.None);
            return result;
        }

        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            var csvFile = await provider.Contents[0].ReadAsByteArrayAsync();
            using (var fs = new System.IO.FileStream(StaticStringsProvider.GetPathtoCsvFile(), System.IO.FileMode.OpenOrCreate))
            {
                await fs.WriteAsync(csvFile, 0, csvFile.Length);
            }
            StaticStringsProvider.ReadStaticStrings();
            return Ok("Upload done");
        }
    }
}
