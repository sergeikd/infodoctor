using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ResortReviewService : IResortReviewService
    {
        private readonly IResortReviewRepository _reviewRepository;
        private readonly IResortRepository _resortRepository;
        private readonly ILanguageRepository _languageRepository;

        public ResortReviewService(IResortReviewRepository reviewRepository, IResortRepository resortRepository, ILanguageRepository languageRepository)
        {
            if (reviewRepository == null) throw new ArgumentNullException(nameof(reviewRepository));
            if (resortRepository == null) throw new ArgumentNullException(nameof(resortRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));
            _reviewRepository = reviewRepository;
            _resortRepository = resortRepository;
            _languageRepository = languageRepository;
        }

        public IEnumerable<DtoResortReview> GetAllReviews()
        {
            var resReviewList = _reviewRepository.GetResortReviews().ToList();
            var dtoResReviewList = new List<DtoResortReview>();

            foreach (var review in resReviewList)
            {
                dtoResReviewList.Add(
                    new DtoResortReview()
                    {
                        Id = review.Id,
                        Text = review.Text,
                        UserName = review.UserName,
                        UserId = review.UserId,
                        PublishTime = review.PublishTime,
                        RatePrice = review.RatePrice,
                        RateQuality = review.RateQuality,
                        RatePoliteness = review.RatePoliteness,
                        ResortId = review.Resort.Id,
                        LangCode = review.Language.Code
                    }
                );
            }

            return dtoResReviewList;
        }

        public IEnumerable<DtoResortReview> GetReviewsByResortId(int id)
        {
            var resReviewList = _reviewRepository.GetResortReviewsByClinicId(id).ToList();
            var dtoResReviewList = new List<DtoResortReview>();

            foreach (var review in resReviewList)
            {
                dtoResReviewList.Add(
                    new DtoResortReview()
                    {
                        Id = review.Id,
                        Text = review.Text,
                        UserName = review.UserName,
                        UserId = review.UserId,
                        PublishTime = review.PublishTime,
                        RatePrice = review.RatePrice,
                        RateQuality = review.RateQuality,
                        RatePoliteness = review.RatePoliteness,
                        ResortId = review.Resort.Id,
                        LangCode = review.Language.Code
                    }
                );
            }

            return dtoResReviewList;
        }

        public DtoPagedResortReview GetPagedReviewByResortId(int id, int perPage, int numPage)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }

            var reviews = _reviewRepository.GetResortReviewsByClinicId(id).Where(r => r.IsApproved);

            var pagedList = new PagedList<ResortReview>(reviews, perPage, numPage);
            if (!pagedList.Any())
            {
                throw new ApplicationException("Page not found");
            }

            var dtoResReviewList = new List<DtoResortReview>();

            foreach (var review in pagedList)
            {
                dtoResReviewList.Add(
                    new DtoResortReview()
                    {
                        Id = review.Id,
                        Text = review.Text,
                        UserName = review.UserName,
                        UserId = review.UserId,
                        PublishTime = review.PublishTime,
                        RatePrice = review.RatePrice,
                        RateQuality = review.RateQuality,
                        RatePoliteness = review.RatePoliteness,
                        ResortId = review.Resort.Id,
                        LangCode = review.Language.Code
                    }
                );
            }

            var result = new DtoPagedResortReview()
            {
                ResortReviews = dtoResReviewList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }

        public DtoPagedResortReview GetPagedReviewByResortId(int id, int perPage, int numPage,string lang)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            lang = lang.ToLower();

            var reviews = _reviewRepository.GetResortReviewsByClinicId(id).Where(c => c.IsApproved)
                .OrderByDescending(c => c.Language.Code.ToLower() == lang)
                .ThenByDescending(c => c.Language.Code.ToLower() != lang)
                .ThenByDescending(c => c.PublishTime);

            var pagedList = new PagedList<ResortReview>(reviews, perPage, numPage);
            if (!pagedList.Any())
            {
                throw new ApplicationException("Page not found");
            }

            var dtoResReviewList = new List<DtoResortReview>();

            foreach (var review in pagedList)
            {
                dtoResReviewList.Add(
                    new DtoResortReview()
                    {
                        Id = review.Id,
                        Text = review.Text,
                        UserName = review.UserName,
                        UserId = review.UserId,
                        PublishTime = review.PublishTime,
                        RatePrice = review.RatePrice,
                        RateQuality = review.RateQuality,
                        RatePoliteness = review.RatePoliteness,
                        ResortId = review.Resort.Id,
                        LangCode = review.Language.Code
                    }
                );
            }

            var result = new DtoPagedResortReview()
            {
                ResortReviews = dtoResReviewList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }

        public DtoResortReview GetResortReviewById(int id)
        {
            var review = _reviewRepository.GetReviewById(id);
            var dtoReview = new DtoResortReview()
            {
                Id = review.Id,
                Text = review.Text,
                UserName = review.UserName,
                UserId = review.UserId,
                PublishTime = review.PublishTime,
                RatePrice = review.RatePrice,
                RateQuality = review.RateQuality,
                RatePoliteness = review.RatePoliteness,
                ResortId = review.Resort.Id,
                LangCode = review.Language.Code
            };

            return dtoReview;
        }

        public void Add(DtoResortReview review)
        {
            if (review.UserId == null || review.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            Resort resort;
            try
            {
                resort = _resortRepository.GetResortById(review.ResortId);
            }
            catch
            {
                throw new ApplicationException("Resort not found");
            }
            if (review.Text == "" || review.ResortId == 0 || review.RatePoliteness < 1 ||
                review.RatePrice < 1 || review.RateQuality < 1 || review.Text == string.Empty)
                throw new ApplicationException("Incorrect data, pussible some required fields are null or empty");

            var lang = _languageRepository.GetLanguageByCode(review.LangCode);

            var newReview = new ResortReview()
            {
                Text = review.Text,
                UserName = review.UserName,
                UserId = review.UserId,
                PublishTime = review.PublishTime,
                RatePoliteness = review.RatePoliteness,
                RatePrice = review.RatePrice,
                RateQuality = review.RateQuality,
                Resort = resort,
                IsApproved = true, //TODO: change to false when moderator control will be done
                Language = lang
            };

            _reviewRepository.Add(newReview);
        }

        public void Update(DtoResortReview review)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ResortReview review;
            try
            {
                review = _reviewRepository.GetReviewById(id);
            }
            catch
            {
                throw new ApplicationException("Review not found");
            }
            if (review != null)
                _reviewRepository.Remove(review);
        }
    }
}
