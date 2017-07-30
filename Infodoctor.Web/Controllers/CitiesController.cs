using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;
using Infodoctor.Web.Models;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class CitiesController : ApiController
    {
        private readonly ICitiesService _citiesService;
        private readonly ConfigService _configService;

        public CitiesController(ICitiesService citiesService, ConfigService configService)
        {
            if (citiesService == null)
                throw new ArgumentNullException(nameof(citiesService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _citiesService = citiesService;
            _configService = configService;
        }

        // GET api/cities
        [AllowAnonymous]
        [Route("api/{lang}/cities")]
        public IEnumerable<DtoCitySingleLang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _citiesService.GetAllCities(lang);
        }

        // GET api/cities/5
        [AllowAnonymous]
        [Route("api/{lang}/cities")]
        public DtoCitySingleLang Get(int id, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _citiesService.GetCity(id, lang);
        }

        // GET: api/cities/clinicused
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/cities/clinicused")]
        public IEnumerable<DtoCitySingleLang> ClinicUsed(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _citiesService.GetCitiesWithClinics(lang);
        }

        // POST api/cities
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]CityPostBindingModel model)
        {
            throw new NotImplementedException();
            //_citiesService.Add(model.Name, land);
        }

        // PUT api/cities/5
        [Authorize(Roles = "admin, moder")]
        public void Put([FromBody]CityPostBindingModel model)
        {
            throw new NotImplementedException();
            //_citiesService.Update(model.Id, model.Name, lang);
        }

        // DELETE api/cities/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _citiesService.Delete(id);
        }
    }
}