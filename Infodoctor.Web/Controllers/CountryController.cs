using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain.Entities;

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
        public IEnumerable<Country> Get()
        {
            var countries = _countryService.GetAllCountries().ToList();
            return countries;
        }

        // GET: api/Country/5
        [AllowAnonymous]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Country
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Country/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Country/5
        public void Delete(int id)
        {
        }
    }
}
