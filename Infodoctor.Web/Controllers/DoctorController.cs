using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.Web.Infrastructure.Interfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IConfigService _configService;

        public DoctorController(IDoctorService doctorService, IConfigService configService)
        {
            if (doctorService == null)
                throw new ArgumentNullException(nameof(doctorService));
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));
            _configService = configService;
            _doctorService = doctorService;
        }

        // GET api/doctor
        [AllowAnonymous]
        public IEnumerable<DtoDoctor> Get()
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var doctors = _doctorService.GetAllDoctors().ToArray();
            for (var i = 0; i < doctors.Length; i++)
                doctors[i].ImagePath = baseUrl + _configService.PathToDoctorsImages + '/' + doctors[i].ImageName;

            return doctors;
        }

        // GET api/doctor/5
        [AllowAnonymous]
        public DtoDoctor Get(int id)
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var doctor = _doctorService.GetDoctorById(id);
            doctor.ImagePath = baseUrl + _configService.PathToDoctorsImages + '/' + doctor.ImageName;

            return doctor;
        }

        // GET: api/doctor/page/perPage/numPage 
        [Route("api/doctor/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        [AllowAnonymous]
        public DtoPagedDoctor GetPage(int perPage, int numPage)
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var pagedDoctor = _doctorService.GetPagedDoctors(perPage, numPage);
            var doctors = pagedDoctor.Doctors.ToArray();     

            for (var i = 0; i < doctors.Length; i++)
                doctors[i].ImagePath = baseUrl + _configService.PathToDoctorsImages + '/' + doctors[i].ImageName;

            pagedDoctor.Doctors = doctors.ToList();

            return pagedDoctor;
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