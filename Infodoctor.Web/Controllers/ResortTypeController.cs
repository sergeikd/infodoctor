using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;

namespace Infodoctor.Web.Controllers
{
    public class ResortTypeController : ApiController
    {
        private readonly IResortTypeService _resortTypeService;

        public ResortTypeController(IResortTypeService resortTypeService)
        {
            if (resortTypeService == null) throw new ArgumentNullException(nameof(resortTypeService));
            _resortTypeService = resortTypeService;
        }

        // GET api/<controller>
        [Route("api/{lang}/ResortType")]
        public IEnumerable<DtoResortTypeSingleLang> Get(string lang)
        {
            return _resortTypeService.GetTypes(lang);
        }

        // GET api/<controller>/5
        [Route("api/{lang}/ResortType")]
        public DtoResortTypeSingleLang Get(int id,string lang)
        {
            return _resortTypeService.GetType(id,lang);
        }

        // GET api/<controller>/5
        [Route("api/ResortType")]
        public DtoResortTypeMultiLang Get(int id)
        {
            return _resortTypeService.GetType(id);
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
        [Route("api/ResortType")]
        public void Delete(int id)
        {
            _resortTypeService.Delete(id);
        }
    }
}