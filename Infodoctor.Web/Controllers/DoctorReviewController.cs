using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain.Entities;
using Infodoctor.Web.Models;
using Microsoft.AspNet.Identity;


namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class DoctorReviewController : ApiController
    {
        private readonly IDoctorReviewService _doctorReviewService;

        public DoctorReviewController(IDoctorReviewService doctorReviewService)
        {
            if(doctorReviewService == null)
                throw new ArgumentNullException(nameof(doctorReviewService));
            _doctorReviewService = doctorReviewService;
        }

        [AllowAnonymous]
        // GET api/doctorreview
        public IEnumerable<DoctorReview> Get()
        {
            return _doctorReviewService.GetAllReviews();
        }

        [AllowAnonymous]
        // GET api/doctorreview/5
        public DoctorReview Get(int id)
        {
            return _doctorReviewService.GetReviewById(id);
        }

        // GET: api/doctorreview/page/doctorId/perPage/numPage 
        [AllowAnonymous]
        [Route("api/DoctorReview/page/{doctorId:int}/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedDoctorReview GetPaged(int doctorId, int perPage, int numPage)
        {
            return _doctorReviewService.GetPagedReviewByDoctorId(doctorId, perPage, numPage);
        }

        [Authorize(Roles = "admin, moder")]
        // POST api/doctorreview
        public void Post([FromBody]DoctorReviewBindingModel doctorReview)
        {
            if (doctorReview == null)
            {
                throw new ApplicationException("Incorrect review object");
            }

            var review = new DoctorReview()
            {
                DoctorId = doctorReview.DoctorId,
                RatePoliteness = doctorReview.RatePoliteness,
                RatePrice = doctorReview.RatePrice,
                RateQuality = doctorReview.RateQuality,
                Text = doctorReview.Text,
                PublishTime = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName()
            };
            _doctorReviewService.Add(review);
        }

        [Authorize(Roles = "admin, moder")]
        // PUT api/doctorreview/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [Authorize(Roles = "admin, moder")]
        // DELETE api/doctorreview/5
        public void Delete(int id)
        {
            _doctorReviewService.Delete(id);
        }
    }
}