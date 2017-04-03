using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.Web.Models;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class CountryController : ApiController
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            if (countryService == null)
            {
                throw new ArgumentNullException(nameof(countryService));
            }
            _countryService = countryService;
        }

        // GET: api/Country
        [AllowAnonymous]
        public IEnumerable<DtoCountry> Get()
        {
            return _countryService.GetAllCountries();
        }

        // GET: api/Country/5
        [AllowAnonymous]
        public DtoCountry Get(int id)
        {
            return _countryService.GetCountryById(id);
        }

        // POST: api/Country
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]CountryPostBindingModel value)
        {
            var newCountry = new DtoCountry()
            {
                Name = value.Name,
                CitiesId = value.CitiesId
            };
            _countryService.Add(newCountry);
        }

        // PUT: api/Country/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]CountryPostBindingModel value)
        {
            var newCountry = new DtoCountry()
            {
                Id = id,
                Name = value.Name,
                CitiesId = value.CitiesId
            };
            _countryService.Update(newCountry);
        }

        // DELETE: api/Country/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _countryService.Delete(id);
        }
    }
}
