﻿using System;
using System.Collections.Generic;
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
        public IEnumerable<DtoDoctor> Get()
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            var doctors = _doctorService.GetAllDoctors(pathToImage);
            return doctors;
        }

        // GET api/doctor/5
        [AllowAnonymous]
        public DtoDoctor Get(int id)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _doctorService.GetDoctorById(id, pathToImage);
        }

        // GET: api/doctor/page/perPage/numPage 
        [Route("api/doctor/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        [AllowAnonymous]
        public DtoPagedDoctor GetPage(int perPage, int numPage)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _doctorService.GetPagedDoctors(perPage, numPage, pathToImage);
        }

        // api/clinic/doctor/perPage/numPage
        [AllowAnonymous]
        [Route("api/doctor/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedDoctor SearchClinic(int perPage, int numPage, [FromBody]DtoDoctorSearchModel searchModel)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            var pagedDoctors = _doctorService.SearchDoctors(perPage, numPage, searchModel, pathToImage);

            return pagedDoctors;
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