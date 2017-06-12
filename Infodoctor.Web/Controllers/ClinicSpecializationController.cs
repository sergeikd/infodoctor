using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ClinicSpecializationController : ApiController
    {
        private readonly IClinicSpecializationService _clinicSpecializationService;
        private readonly ConfigService _configService;

        public ClinicSpecializationController(IClinicSpecializationService clinicSpecializationService, ConfigService configService)
        {
            if (clinicSpecializationService == null)
                throw new ArgumentNullException(nameof(clinicSpecializationService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _clinicSpecializationService = clinicSpecializationService;
            _configService = configService;
        }

        // GET api/ru/clinicspecialization
        [AllowAnonymous]
        [HttpGet]
        [Route("api/{lang}/clinicspecialization")]
        public IEnumerable<DtoClinicSpecializationMultilang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;

            return _clinicSpecializationService.GetAllSpecializations(lang);
        }

        // GET api/clinicspecialization/5
        [AllowAnonymous]
        [HttpGet]
        [Route("api/{lang}/clinicspecialization/{id:int}")]
        public DtoClinicSpecializationMultilang Get(int id, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;

            return _clinicSpecializationService.GetSpecializationById(id, lang);
        }

        // POST api/clinicspecialization
        [Authorize(Roles = "admin, moder")]
        [HttpPost]
        public void Post([FromBody]DtoClinicSpecializationMultilang model)
        {
            _clinicSpecializationService.Add(model);
        }

        // PUT api/clinicspecialization/5
        [Authorize(Roles = "admin, moder")]
        [HttpPut]
        public void Put([FromBody]DtoClinicSpecializationMultilang model)
        {
            _clinicSpecializationService.Update(model);
        }

        // DELETE api/clinicspecialization/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _clinicSpecializationService.Delete(id);
        }
    }
}