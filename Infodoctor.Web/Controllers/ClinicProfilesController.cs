using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ClinicProfilesController : ApiController
    {
        private readonly IClinicProfilesService _clinicProfilesService;

        public ClinicProfilesController(IClinicProfilesService clinicProfilesService)
        {
            if (clinicProfilesService == null)
                throw new ArgumentNullException(nameof(clinicProfilesService));
            _clinicProfilesService = clinicProfilesService;
        }

        // GET api/<controller>
        public IEnumerable<ClinicProfile> Get()
        {
            return _clinicProfilesService.GetAllProfiles().ToList();
        }

        // GET api/<controller>/5
        public ClinicProfile Get(int id)
        {
            return _clinicProfilesService.GetProfileById(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}