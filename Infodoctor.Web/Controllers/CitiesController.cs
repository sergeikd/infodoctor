using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
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
        public IEnumerable<DtoCity> Get()
        {
            return _citiesService.GetAllCities();
        }

        // GET api/cities/5
        [AllowAnonymous]
        public DtoCity Get(int id)
        {
            return _citiesService.GetCityById(id);
        }

        // GET: api/cities/clinicused
        [Route("api/cities/clinicused")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<DtoCity> ClinicUsed()
        {
            return _citiesService.GetCitiesWithClinics();
        }

        // POST api/cities
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]CityPostBindingModel value)
        {
            _citiesService.Add(value.Name);
        }

        // PUT api/cities/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
            _citiesService.Update(id,value);
        }

        // DELETE api/cities/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _citiesService.Delete(id);
        }
    }
}