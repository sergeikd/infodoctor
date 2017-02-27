using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ClinicSpecializationController : ApiController
    {
        private readonly IClinicSpecializationService _clinicSpecializationService;

        public ClinicSpecializationController(IClinicSpecializationService clinicSpecializationService)
        {
            if (clinicSpecializationServic == null)
                throw new ArgumentNullException(nameof(clinicSpecializationService));
            _clinicSpecializationService = clinicSpecializationService;
        }

        // GET api/clinicprofiles
        public IEnumerable<ClinicProfile> Get()
        {
            return _clinicProfilesService.GetAllProfiles().ToList();
        }

        // GET api/clinicprofiles/5
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