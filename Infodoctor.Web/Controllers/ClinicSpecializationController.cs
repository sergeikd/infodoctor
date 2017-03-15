using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ClinicSpecializationController : ApiController
    {
        private readonly IClinicSpecializationService _clinicSpecializationService;

        public ClinicSpecializationController(IClinicSpecializationService clinicSpecializationService)
        {
            if (clinicSpecializationService == null)
                throw new ArgumentNullException(nameof(clinicSpecializationService));
            _clinicSpecializationService = clinicSpecializationService;
        }

        // GET api/clinicspecialization
        public IEnumerable<ClinicSpecialization> Get()
        {
            return _clinicSpecializationService.GetAllSpecializations();
        }

        // GET api/clinicspecialization/5
        public ClinicSpecialization Get(int id)
        {
            return _clinicSpecializationService.GetSpecializationById(id);
        }

        // POST api/clinicspecialization
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
            _clinicSpecializationService.Add(value);
        }

        // PUT api/clinicspecialization/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
            _clinicSpecializationService.Update(id, value);
        }

        // DELETE api/clinicspecialization/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _clinicSpecializationService.Delete(id);
        }
    }
}