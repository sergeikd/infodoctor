using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class CountryController : ApiController
    {
        private readonly ICountryService _countryService;
        private readonly ConfigService _configService;
        public CountryController(ICountryService countryService, ConfigService configService)
        {
            if (countryService == null) throw new ArgumentNullException(nameof(countryService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _countryService = countryService;
            _configService = configService;
        }

        // GET: api/Country
        [AllowAnonymous]
        [Route("api/{lang}/Country")]
        public IEnumerable<DtoCountrySingleLang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _countryService.GetAllCountries(lang);
        }

        // GET: api/Country/5
        [AllowAnonymous]
        [Route("api/{lang}/Country")]
        public DtoCountrySingleLang Get(string lang, int id)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _countryService.GetCountryById(id, lang);
        }

        // POST: api/Country
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]DtoCountryMultiLang value)
        {
            _countryService.Add(value);
        }

        // PUT: api/Country/5
        [Authorize(Roles = "admin, moder")]
        public void Put([FromBody]DtoCountryMultiLang value)
        {
            _countryService.Update(value);
        }

        // DELETE: api/Country/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _countryService.Delete(id);
        }
    }
}
