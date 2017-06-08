using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Models;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class CitiesController : ApiController
    {
        private readonly ICitiesService _citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            if (citiesService == null)
                throw new ArgumentNullException(nameof(citiesService));
            _citiesService = citiesService;
        }

        // GET api/cities
        [AllowAnonymous]
        public IEnumerable<DtoCity> Get(string lang = "ru")
        {
            return _citiesService.GetAllCities(lang);
        }

        // GET api/cities/5
        [AllowAnonymous]
        public DtoCity Get(int id, string lang = "ru")
        {
            return _citiesService.GetCityById(id, lang);
        }

        // GET: api/cities/clinicused
        [Route("api/cities/clinicused")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<DtoCity> ClinicUsed(string lang = "ru")
        {
            return _citiesService.GetCitiesWithClinics(lang);
        }

        // POST api/cities
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]CityPostBindingModel model, string land = "ru")
        {
            _citiesService.Add(model.Name, land);
        }

        // PUT api/cities/5
        [Authorize(Roles = "admin, moder")]
        public void Put([FromBody]CityPostBindingModel model, string lang = "ru")
        {
            _citiesService.Update(model.Id, model.Name, lang);
        }

        // DELETE api/cities/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _citiesService.Delete(id);
        }
    }
}