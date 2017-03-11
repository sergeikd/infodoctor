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
    public class ClinicReviewController : ApiController
    {
        private readonly IClinicReviewService _clinicReviewService;

        public ClinicReviewController(IClinicReviewService clinicReviewService)
        {
            if (clinicReviewService == null)
                throw new ArgumentNullException(nameof(clinicReviewService));
            _clinicReviewService = clinicReviewService;
        }
        // GET: api/ClinicReview
        [Authorize(Roles = "admin, moder")]
        public IEnumerable<ClinicReview> Get()
        {
            return _clinicReviewService.GetClinicReviews();
        }

        // GET: api/ClinicReview/5
        [Authorize]
        public ClinicReview Get(int id)
        {
            return _clinicReviewService.GetClinicReviewById(id);
        }


        // GET: api/ClinicReview/page/clinicId/perPage/numPage 
        [AllowAnonymous]
        [Route("api/ClinicReview/page/{clinicId:int}/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedClinicReview GetPaged(int clinicId, int perPage, int numPage)
        {
            return _clinicReviewService.GetPagedReviewsByClinicId(clinicId, perPage, numPage);
        }

        // POST: api/ClinicReview
        [Authorize]
        public void Post([FromBody]ClinicReviewBindingModel clinicReview)
        {
            //TODO: extend Review class with bool IsApproved property when moderators will be able to check each review
            //TODO: make service for e-mail sending to all moderators whether new review posted

            if (clinicReview == null)
            {
                throw new ApplicationException("Incorrect review object");
            }
            var review = new ClinicReview
            {
                ClinicId = clinicReview.ClinicId,
                RatePoliteness = clinicReview.RatePoliteness,
                RatePrice = clinicReview.RatePrice,
                RateQuality = clinicReview.RateQuality,
                Text = clinicReview.Text,
                PublishTime = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName()
            };
            _clinicReviewService.Add(review);
        }

        // PUT: api/ClinicReview/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ClinicReview/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _clinicReviewService.Delete(id);
        }
    }
}
