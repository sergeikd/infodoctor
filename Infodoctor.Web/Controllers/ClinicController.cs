using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;

namespace Infodoctor.Web.Controllers
{
    public class ClinicController : ApiController
    {
        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinicService)
        {
            if (clinicService == null)
                throw new ArgumentNullException(nameof(clinicService));
            _clinicService = clinicService;
        }

        // GET: api/Clinic
        public IEnumerable<DtoClinic> Get()
        {
            return _clinicService.GetAllClinics();
        }

        // GET: api/Clinic/page/perPage/numPage 
        [Route("api/Clinic/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedClinic GetPage(int perPage, int numPage)
        {
            return _clinicService.GetPagedClinics(perPage, numPage);
        }
        // GET: api/Clinic/5
        public DtoClinic Get(int id)
        {
            return _clinicService.GetClinicById(id);
        }

        // POST: api/Clinic
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clinic/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clinic/5
        public void Delete(int id)
        {
        }
    }
}
