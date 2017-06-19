using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorCategoriesController : ApiController
    {
        private readonly IDoctorCategoryService _doctorCategoryService;
        private readonly ConfigService _configService;

        public DoctorCategoriesController(IDoctorCategoryService doctorCategoryService, ConfigService configService)
        {
            if (doctorCategoryService == null)
                throw new ArgumentNullException(nameof(doctorCategoryService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _doctorCategoryService = doctorCategoryService;
            _configService = configService;
        }

        // GET api/doctorcategories
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/doctorcategories")]
        public IEnumerable<DtoDoctorCategorySingleLang> Get(string lang)
        {
            lang = !string.IsNullOrEmpty(lang) ? lang : _configService.DefaultLangCode;
            return _doctorCategoryService.GetAllCategories(lang);
        }

        // GET api/doctorcategories/5
        [HttpGet]
        [AllowAnonymous]
        [Route("api/{lang}/doctorcategories/{id}")]
        public DtoDoctorCategorySingleLang Get(int id, string lang)
        {
            lang = !string.IsNullOrEmpty(lang) ? lang : _configService.DefaultLangCode;
            return _doctorCategoryService.GetCategoryById(id, lang);
        }

        // POST api/doctorcategories
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
            //_doctorCategoryService.Add(value);
        }

        // PUT api/doctorcategories/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
            //_doctorCategoryService.Update(id,value);
        }

        // DELETE api/doctorcategories/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
            //_doctorCategoryService.Delete(id);
        }
    }
}