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
    public class ClinicReviewController : ApiController
    {
        private readonly IClinicReviewService _clinicReviewService;
        private readonly IYandexTranslateService _translateService;
        private readonly ConfigService _configService;

        public ClinicReviewController(IClinicReviewService clinicReviewService, IYandexTranslateService translateService, ConfigService configService)
        {
            if (clinicReviewService == null)
                throw new ArgumentNullException(nameof(clinicReviewService));
            if (translateService == null) throw new ArgumentNullException(nameof(translateService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _clinicReviewService = clinicReviewService;
            _translateService = translateService;
            _configService = configService;
        }
        // GET: api/ClinicReview
        [Authorize(Roles = "admin, moder")]
        public IEnumerable<DtoClinicReview> Get()
        {
            return _clinicReviewService.GetClinicReviews();
        }

        // GET: api/ClinicReview/5
        [Authorize]
        public DtoClinicReview Get(int id)
        {
            return _clinicReviewService.GetClinicReviewById(id);
        }


        // GET: api/ClinicReview/page/clinicId/perPage/numPage 
        [AllowAnonymous]
        [Route("api/{lang}/ClinicReview/page/{clinicId:int}/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedClinicReview GetPaged(int clinicId, int perPage, int numPage, string lang)
        {
            return _clinicReviewService.GetPagedReviewsByClinicId(clinicId, perPage, numPage, lang);
        }

        // POST: api/ClinicReview
        [Authorize]
        public void Post([FromBody]ClinicReviewBindingModel clinicReview)
        {
            //TODO: extend Review class with bool IsApproved property when moderators will be able to check each review
            //TODO: make service for e-mail sending to all moderators whether new review posted

            if (clinicReview == null)
                throw new ApplicationException("Incorrect review object");

            var review = new DtoClinicReview
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

            var apiKey = _configService.YandexTranslateApiKey;
            var textLang = _translateService.DetectLang(apiKey, clinicReview.Text);

            review.LangCode = textLang != null ? textLang.Lang : clinicReview.LangCode;

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
