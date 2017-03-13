using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.Web.Infrastructure.Interfaces;

namespace Infodoctor.Web.Controllers
{
    public class ClinicController : ApiController
    {
        private readonly IClinicService _clinicService;
        private readonly IConfigService _configService;

        public ClinicController(IClinicService clinicService, IConfigService configService)
        {
            if (clinicService == null)
                throw new ArgumentNullException(nameof(clinicService));
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));
            _configService = configService;
            _clinicService = clinicService;
        }

        // GET: api/Clinic
        public IEnumerable<DtoClinic> Get()
        {
            var clinics = _clinicService.GetAllClinics().ToArray();
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            for (var i = 0; i < clinics.Length; i++)
            {
                clinics[i].ImagePath = baseUrl + _configService.PathToClinicsImages + '/' + clinics[i].ImageName;
            }

            return clinics;
        }

        // GET: api/Clinic/page/perPage/numPage
        [Route("api/Clinic/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedClinic GetPage(int perPage, int numPage)
        {
            var pages = _clinicService.GetPagedClinics(perPage, numPage);
            var clinics = pages.Clinics.ToArray();
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            for (var i = 0; i < clinics.Length; i++)
                clinics[i].ImagePath = baseUrl + _configService.PathToClinicsImages + '/' + clinics[i].ImageName;

            pages.Clinics = clinics.ToList();

            return pages;
        }

        // api/clinic/search/perPage/numPage
        [Route("api/clinic/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedClinic SearchClinic(int perPage, int numPage, [FromBody]DtoClinicSearchModel searchModel)
        {
            var pagedClinic = _clinicService.SearchClinics(perPage, numPage, searchModel);

            var clinics = pagedClinic.Clinics.ToArray();
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            for (var i = 0; i < clinics.Length; i++)
                clinics[i].ImagePath = baseUrl + _configService.PathToClinicsImages + '/' + clinics[i].ImageName;

            pagedClinic.Clinics = clinics.ToList();

            return pagedClinic;
        }
        // GET: api/Clinic/5 
        public DtoClinic Get(int id)
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var clinic = _clinicService.GetClinicById(id);
            clinic.ImagePath = baseUrl + _configService.PathToClinicsImages + '/' + clinic.ImageName;

            return clinic;
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
