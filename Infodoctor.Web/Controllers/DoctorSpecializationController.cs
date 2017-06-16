using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorSpecializationController : ApiController
    {
        private readonly IDoctorSpecializationService _doctorSpecializationService;
        private readonly ConfigService _configService;

        public DoctorSpecializationController(IDoctorSpecializationService doctorSpecializationService, ConfigService configService)
        {
            if (doctorSpecializationService == null)
                throw new ArgumentNullException(nameof(doctorSpecializationService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _doctorSpecializationService = doctorSpecializationService;
            _configService = configService;
        }

        // GET api/doctorspecialization
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/doctorspecialization")]
        public IEnumerable<DtoDoctorSpecializationSilngleLang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _doctorSpecializationService.GetAllSpecializations(lang);
        }

        // GET api/doctorspecialization/5
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/doctorspecialization/{id:int}")]
        public DtoDoctorSpecializationSilngleLang Get(int id, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            return _doctorSpecializationService.GetSpecializationById(id, lang);
        }

        [Authorize(Roles = "admin, moder")]
        // POST api/doctorspecialization
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
            //_doctorSpecializationService.Add(value);
        }

        [Authorize(Roles = "admin, moder")]
        // PUT api/doctorspecialization/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
            //_doctorSpecializationService.Update(id, value);
        }


        // DELETE api/doctorspecialization/5
        public void Delete(int id)
        {
            _doctorSpecializationService.Delete(id);
        }
    }
}