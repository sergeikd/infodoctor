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

        public ResortReviewService(IResortReviewRepository reviewRepository, IResortRepository resortRepository)
        {
            if (reviewRepository == null) throw new ArgumentNullException(nameof(reviewRepository));
            if (resortRepository == null) throw new ArgumentNullException(nameof(resortRepository));
            _reviewRepository = reviewRepository;
            _resortRepository = resortRepository;
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
                        ResortId = review.Resort.Id
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
                        ResortId = review.Resort.Id
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
                        ResortId = review.Resort.Id
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
                ResortId = review.Resort.Id
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
                IsApproved = true //TODO: change to false when moderator control will be done
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
