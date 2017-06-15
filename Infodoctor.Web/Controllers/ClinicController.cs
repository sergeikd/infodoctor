using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ClinicController : ApiController
    {
        private readonly IClinicService _clinicService;
        private readonly ConfigService _configService;
        public ClinicController(IClinicService clinicService, ConfigService configService)
        {
            if (clinicService == null)
                throw new ArgumentNullException(nameof(clinicService));
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));
            _configService = configService;
            _clinicService = clinicService;
        }

        // GET: api/Clinic
        [AllowAnonymous]
        [HttpGet]
        [Route("api/{lang}/Clinic")]
        public IEnumerable<DtoClinicSingleLang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            var clinics = _clinicService.GetAllClinics(pathToImage, lang);

            return clinics;
        }

        // GET: api/Clinic/5 
        [AllowAnonymous]
        [HttpGet]
        [Route("api/{lang}/Clinic/{id:int}")]
        public DtoClinicSingleLang Get(string lang, int id)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            var clinic = _clinicService.GetClinicById(id, pathToImage, lang);

            return clinic;
        }

        // GET: api/Clinic/page/perPage/numPage
        [AllowAnonymous]
        [Route("api/{lang}/Clinic/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedClinic GetPage(string lang, int perPage, int numPage)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _clinicService.GetPagedClinics(perPage, numPage, pathToImage, lang);
        }

        // api/clinic/search/perPage/numPage
        [AllowAnonymous]
        [Route("api/{lang}/clinic/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedClinic SearchClinic(int perPage, int numPage, [FromBody]DtoClinicSearchModel searchModel, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            var pagedClinic = _clinicService.SearchClinics(perPage, numPage, searchModel, pathToImage, lang);

            return pagedClinic;
        }

        // POST: api/Clinic
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clinic/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clinic/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
        }
    }
}
