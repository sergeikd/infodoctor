using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;
using Infodoctor.Web.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IEnumerable<DtoClinic> Get()
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _clinicService.GetAllClinics(pathToImage);
        }

        // GET: api/Clinic/page/perPage/numPage
        [AllowAnonymous]
        [Route("api/Clinic/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedClinic GetPage(int perPage, int numPage)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _clinicService.GetPagedClinics(perPage, numPage, pathToImage);
        }

        // api/clinic/search/perPage/numPage
        [AllowAnonymous]
        [Route("api/clinic/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedClinic SearchClinic(int perPage, int numPage, [FromBody]DtoClinicSearchModel searchModel)
        {
            var  pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            var pagedClinic = _clinicService.SearchClinics(perPage, numPage, searchModel, pathToImage);

            return pagedClinic;
        }

        // GET: api/Clinic/5 
        [AllowAnonymous]
        public DtoClinic Get(int id)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _clinicService.GetClinicById(id, pathToImage);
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
