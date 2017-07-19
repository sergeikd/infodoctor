using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.Web.Controllers
{
    public class ClinicTypeController : ApiController
    {
        private readonly IClinicTypeService _clinicTypeService;

        public ClinicTypeController(IClinicTypeService clinicTypeService)
        {
            if (clinicTypeService == null) throw new ArgumentNullException(nameof(clinicTypeService));
            _clinicTypeService = clinicTypeService;
        }

        // GET api/ClinicType
        [Route("api/{lang}/ClinicType")]
        public IEnumerable<DtoClinicTypeSingleLang> Get(string lang)
        {
            return _clinicTypeService.GetTypes(lang);
        }

        // GET api/ClinicType/5
        [Route("api/{lang}/ClinicType/")]
        public DtoClinicTypeSingleLang Get(int id, string lang)
        {
            return _clinicTypeService.GetType(id, lang);
        }

        // GET api/ClinicType/5
        [Route("api/ClinicType/")]
        public DtoClinicTypeMultiLang Get(int id)
        {
            return _clinicTypeService.GetType(id);
        }

        // POST api/ClinicType
        public void Post([FromBody]string value)
        {
        }

        // PUT api/ClinicType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ClinicType/5
        [Route("api/ClinicType/")]
        public void Delete(int id)
        {
            _clinicTypeService.Delete(id);
        }
    }
}