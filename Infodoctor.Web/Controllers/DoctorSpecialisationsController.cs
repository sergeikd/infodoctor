using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorSpecialisationsController : ApiController
    {
        private readonly IDoctorSpecializationService _doctorSpecializationService;

        public DoctorSpecialisationsController(IDoctorSpecializationService doctorSpecializationService)
        {
            if (doctorSpecializationService == null)
                throw new ArgumentNullException(nameof(doctorSpecializationService));
            _doctorSpecializationService = doctorSpecializationService;
        }

        // GET api/doctorspecialisations
        [AllowAnonymous]
        public IEnumerable<DtoDoctorSpecialisation> Get()
        {
            return _doctorSpecializationService.GetAllSpecializations();
        }

        [AllowAnonymous]
        // GET api/doctorspecialisations/5
        public DtoDoctorSpecialisation Get(int id)
        {
            return _doctorSpecializationService.GetSpecializationById(id);
        }

        [Authorize(Roles = "admin, moder")]
        // POST api/doctorspecialisations
        public void Post([FromBody]string value)
        {
            _doctorSpecializationService.Add(value);
        }

        [Authorize(Roles = "admin, moder")]
        // PUT api/doctorspecialisations/5
        public void Put(int id, [FromBody]string value)
        {
            _doctorSpecializationService.Update(id, value);
        }


        // DELETE api/doctorspecialisations/5
        public void Delete(int id)
        {
            _doctorSpecializationService.Delete(id);
        }
    }
}