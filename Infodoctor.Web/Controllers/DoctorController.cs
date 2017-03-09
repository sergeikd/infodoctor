using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            if (doctorService == null)
                throw new ArgumentNullException(nameof(doctorService));
            _doctorService = doctorService;
        }

        // GET api/doctor
        [AllowAnonymous]
        public IEnumerable<DtoDoctor> Get()
        {
            return _doctorService.GetAllDoctors();
        }

        // GET api/doctor/5
        [AllowAnonymous]
        public DtoDoctor Get(int id)
        {
            return _doctorService.GetDoctorById(id);
        }

        // GET: api/doctor/page/perPage/numPage 
        [Route("api/doctor/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        [AllowAnonymous]
        public DtoPagedDoctor GetPage(int perPage, int numPage)
        {
            return _doctorService.GetPagedDoctors(perPage, numPage);
        }

        // POST api/doctor
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]DtoDoctor value)
        {
            _doctorService.Add(value);
        }

        // PUT api/doctor/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]DtoDoctor value)
        {
            _doctorService.Update(id,value);
        }

        // DELETE api/doctor/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _doctorService.Delete(id);
        }
    }
}