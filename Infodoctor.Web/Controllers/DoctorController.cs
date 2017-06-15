using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly ConfigService _configService;

        public DoctorController(IDoctorService doctorService, ConfigService configService)
        {
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));
            _configService = configService;
            if (doctorService == null)
                throw new ArgumentNullException(nameof(doctorService));
            _doctorService = doctorService;
        }

        // GET api/doctor
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/Doctor")]
        public IEnumerable<DtoDoctorSingleLang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToDoctorsImages;
            var doctors = _doctorService.GetAllDoctors(pathToImage, lang);
            return doctors;
        }

        // GET api/doctor/5
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/Doctor/{id:int}")]
        public DtoDoctorSingleLang Get(int id,string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToDoctorsImages;
            return _doctorService.GetDoctorById(id, pathToImage, lang);
        }

        // GET: api/doctor/page/perPage/numPage 
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/Doctor/page/{perPage:int}/{numPage:int}")]
        public DtoPagedDoctor GetPage(int perPage, int numPage,string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToDoctorsImages;
            return _doctorService.GetPagedDoctors(perPage, numPage, pathToImage, lang);
        }

        // api/Doctor/doctor/perPage/numPage
        [HttpPost]
        [AllowAnonymous]
        [Route("api/{lang}/Doctor/search/{perPage:int}/{numPage:int}")]      
        public DtoPagedDoctor SearchClinic(int perPage, int numPage, [FromBody]DtoDoctorSearchModel searchModel,string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _configService.DefaultLangCode;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToDoctorsImages;
            var pagedDoctors = _doctorService.SearchDoctors(perPage, numPage, searchModel, pathToImage, lang);

            return pagedDoctors;
        }

        // POST api/doctor
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]DtoDoctorMultiLang value)
        {
            _doctorService.Add(value);
        }

        // PUT api/doctor/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]DtoDoctorMultiLang value)
        {
            _doctorService.Update(id, value);
        }

        // DELETE api/doctor/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _doctorService.Delete(id);
        }
    }
}