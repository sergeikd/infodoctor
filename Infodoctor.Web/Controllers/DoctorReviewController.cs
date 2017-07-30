using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;
using Infodoctor.Web.Models;
using Microsoft.AspNet.Identity;


namespace Infodoctor.Web.Controllers
{
    public class DoctorReviewController : ApiController
    {
        private readonly IDoctorReviewService _doctorReviewService;
        private readonly IYandexTranslateService _translateService;
        private readonly ConfigService _configService;

        public DoctorReviewController(IDoctorReviewService doctorReviewService, IYandexTranslateService translateService, ConfigService configService)
        {
            if(doctorReviewService == null)
                throw new ArgumentNullException(nameof(doctorReviewService));
            if (translateService == null) throw new ArgumentNullException(nameof(translateService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _doctorReviewService = doctorReviewService;
            _translateService = translateService;
            _configService = configService;
        }

        // GET api/doctorreview
        public IEnumerable<DtoDoctorReview> Get()
        {
            return _doctorReviewService.GetAllReviews();
        }

        // GET api/doctorreview/5
        public DtoDoctorReview Get(int id)
        {
            return _doctorReviewService.GetReviewById(id);
        }

        // GET: api/doctorreview/page/doctorId/perPage/numPage 
        [Route("api/{lang}/DoctorReview/page/{doctorId:int}/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedDoctorReview GetPaged(int doctorId, int perPage, int numPage,string lang)
        {
            return _doctorReviewService.GetPagedReviewByDoctorId(doctorId, perPage, numPage,lang);
        }


        // POST api/doctorreview
        [Authorize]
        public void Post([FromBody]DoctorReviewBindingModel doctorReview)
        {
            if (doctorReview == null)
                throw new ApplicationException("Incorrect review object");

            var review = new DtoDoctorReview()
            {
                DoctorId = doctorReview.DoctorId,
                RatePoliteness = doctorReview.RatePoliteness,
                RateProfessionalism = doctorReview.RateProfessionalism,
                RateWaitingTime = doctorReview.RateWaitingTime,
                Text = doctorReview.Text,
                PublishTime = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName()
            };

            var apiKey = _configService.YandexTranslateApiKey;
            var textLang = _translateService.DetectLang(apiKey, doctorReview.Text);

            review.LangCode = textLang != null ? textLang.Lang : doctorReview.LangCode;

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