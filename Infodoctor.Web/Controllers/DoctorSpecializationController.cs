using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorSpecializationController : ApiController
    {
        private readonly IDoctorSpecializationService _doctorSpecializationService;

        public DoctorSpecializationController(IDoctorSpecializationService doctorSpecializationService)
        {
            if (doctorSpecializationService == null)
                throw new ArgumentNullException(nameof(doctorSpecializationService));
            _doctorSpecializationService = doctorSpecializationService;
        }

        // GET api/doctorspecialization
        [AllowAnonymous]
        public IEnumerable<DtoDoctorSpecialization> Get()
        {
            return _doctorSpecializationService.GetAllSpecializations();
        }

        [AllowAnonymous]
        // GET api/doctorspecialization/5
        public DtoDoctorSpecialization Get(int id)
        {
            return _doctorSpecializationService.GetSpecializationById(id);
        }

        [Authorize(Roles = "admin, moder")]
        // POST api/doctorspecialization
        public void Post([FromBody]string value)
        {
            _doctorSpecializationService.Add(value);
        }

        [Authorize(Roles = "admin, moder")]
        // PUT api/doctorspecialization/5
        public void Put(int id, [FromBody]string value)
        {
            _doctorSpecializationService.Update(id, value);
        }


        // DELETE api/doctorspecialization/5
        public void Delete(int id)
        {
            _doctorSpecializationService.Delete(id);
        }
    }
}