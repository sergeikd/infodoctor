using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;

namespace Infodoctor.Web.Controllers
{
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
        public IEnumerable<DtoCity> Get()
        {
            return _citiesService.GetAllCities();
        }

        // GET api/cities/5
        public DtoCity Get(int id)
        {
            return _citiesService.GetCityById(id);
        }

        // GET: api/cities/clinicused
        [Route("api/cities/clinicused")]
        [HttpGet]
        public IEnumerable<DtoCity> ClinicUsed()
        {
            return _citiesService.GetCitiesWithClinics();
        }

        // POST api/cities
        public void Post([FromBody]string value)
        {
            _citiesService.Add(value);
        }

        // PUT api/cities/5
        public void Put(int id, [FromBody]string value)
        {
            _citiesService.Update(id,value);
        }

        // DELETE api/cities/5
        public void Delete(int id)
        {
            _citiesService.Delete(id);
        }
    }
}