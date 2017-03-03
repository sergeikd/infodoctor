using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ClinicReviewService : IClinicReviewService
    {
        private readonly IClinicReviewRepository _clinicReviewRepository;

        public ClinicReviewService(IClinicReviewRepository clinicReviewRepository)
        {
            if (clinicReviewRepository == null)
                throw new ArgumentNullException(nameof(clinicReviewRepository));
            _clinicReviewRepository = clinicReviewRepository;
        }

        public IEnumerable<ClinicReview> GetClinicReviews()
        {
            return _clinicReviewRepository.GetAllClinicReviews();
        }

        public IEnumerable<ClinicReview> GetReviewsByClinicId(int id)
        {
            return _clinicReviewRepository.GetReviewsByClinicId(id);
        }
        public DtoPagedClinicReview GetPagedReviewsByClinicId(int id, int perPage, int numPage)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            var reviews = _clinicReviewRepository.GetReviewsByClinicId(id);
            var pagedList = new PagedList<ClinicReview>(reviews, perPage, numPage);
            if (!pagedList.Any())
            {
                throw new ApplicationException("Page not found");
            }
            var result = new DtoPagedClinicReview
            {
                ClinicReviews = pagedList.ToList(),
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }
        public ClinicReview GetClinicReviewById(int id)
        {
            return _clinicReviewRepository.GetClinicReviewById(id);
        }
        public void Add(ClinicReview clinicReview)
        {
            if(clinicReview.UserId == null || clinicReview.UserName == null)
            {
                throw new UnauthorizedAccessException("Incorrect user's credentials");
            }
            if (clinicReview.Text == "" || clinicReview.ClinicId == 0 || clinicReview.RatePoliteness == 0 ||
                clinicReview.RatePrice == 0 || clinicReview.RateQuality == 0 || clinicReview.Text == string.Empty)
                throw new ApplicationException("Incorrect data, some required fields are null or empty");
        }

        public void Delete(int id)
        {
            ClinicReview deleted;
            try
            {
                deleted = _clinicReviewRepository.GetClinicReviewById(id);
            }
            catch
            {
                throw new ApplicationException("Review not found");
            }
            if (deleted != null)
                _clinicReviewRepository.Delete(deleted);
        }

        public void Update(ClinicReview clinicReview)
        {
            throw new NotImplementedException();
        }
    }
}
