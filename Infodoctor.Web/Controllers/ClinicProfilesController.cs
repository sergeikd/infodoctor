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

        // GET api/clinicprofiles
        [AllowAnonymous]
        public IEnumerable<ClinicProfile> Get()
        {
            return _clinicProfilesService.GetAllProfiles().ToList();
        }

        // GET api/clinicprofiles/5
        [AllowAnonymous]
        public ClinicProfile Get(int id)
        {
            return _clinicProfilesService.GetProfileById(id);
        }

        // POST api/clinicprofiles
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
            _clinicProfilesService.Add(value);
        }

        // PUT api/clinicprofiles/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
            _clinicProfilesService.Update(id, value);
        }

        // DELETE api/clinicprofiles/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _clinicProfilesService.Delete(id);
        }
    }
}