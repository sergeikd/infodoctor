using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorCategoriesController : ApiController
    {
        private readonly IDoctorCategoryService _doctorCategoryService;

        public DoctorCategoriesController(IDoctorCategoryService doctorCategoryService)
        {
            if (doctorCategoryService == null)
                throw new ArgumentNullException(nameof(doctorCategoryService));
            _doctorCategoryService = doctorCategoryService;
        }

        // GET api/doctorcategories
        [AllowAnonymous]
        public IEnumerable<DtoDoctorCategory> Get()
        {
            return _doctorCategoryService.GetAllCategories();
        }

        // GET api/doctorcategories/5
        [AllowAnonymous]
        public DtoDoctorCategory Get(int id)
        {
            return _doctorCategoryService.GetCategoryById(id);
        }

        // POST api/doctorcategories
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
            _doctorCategoryService.Add(value);
        }

        // PUT api/doctorcategories/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
            _doctorCategoryService.Update(id,value);
        }

        // DELETE api/doctorcategories/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _doctorCategoryService.Delete(id);
        }
    }
}