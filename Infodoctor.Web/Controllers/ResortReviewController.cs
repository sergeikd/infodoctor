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
    public class ResortReviewController : ApiController
    {
        private readonly IResortReviewService _reviewService;
        private readonly IYandexTranslateService _translateService;
        private readonly ConfigService _configService;

        public ResortReviewController(IResortReviewService reviewService, IYandexTranslateService translateService, ConfigService configService)
        {
            if (reviewService == null) throw new ArgumentNullException(nameof(reviewService));
            if (translateService == null) throw new ArgumentNullException(nameof(translateService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            _reviewService = reviewService;
            _translateService = translateService;
            _configService = configService;
        }

        // GET api/resortreview
        [Authorize(Roles = "admin, moder")]
        public IEnumerable<DtoResortReview> Get()
        {
            return _reviewService.GetAllReviews();
        }

        // GET api/resortreview/5
        [Authorize(Roles = "admin, moder")]
        public DtoResortReview Get(int id)
        {
            return _reviewService.GetResortReviewById(id);
        }

        // GET: api/resortreview/page/resortId/perPage/numPage 
        [AllowAnonymous]
        [Route("api/{lang}/resortreview/page/{resortId:int}/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedResortReview GetPaged(int resortId, int perPage, int numPage, string lang)
        {
            return _reviewService.GetPagedReviewByResortId(resortId, perPage, numPage, lang);
        }

        // POST api/resortreview
        [Authorize]
        public void Post([FromBody]ResortReviewBindingModel review)
        {
            //TODO: extend Review class with bool IsApproved property when moderators will be able to check each review
            //TODO: make service for e-mail sending to all moderators whether new review posted

            if (review == null)
                throw new ApplicationException("Incorrect review object");

            var newReview = new DtoResortReview()
            {
                ResortId = review.ResortId,
                RatePoliteness = review.RatePoliteness,
                RatePrice = review.RatePrice,
                RateQuality = review.RateQuality,
                Text = review.Text,
                PublishTime = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName()
            };

            var apiKey = _configService.YandexTranslateApiKey;
            var textLang = _translateService.DetectLang(apiKey, review.Text);

            newReview.LangCode = textLang != null ? textLang.Lang : review.LangCode;

            _reviewService.Add(newReview);
        }

        // PUT api/resortreview/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/resortreview/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _reviewService.Delete(id);
        }
    }
}