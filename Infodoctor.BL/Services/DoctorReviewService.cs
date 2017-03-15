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
    public class DoctorReviewService : IDoctorReviewService
    {
        private readonly IDoctorReviewRepository _doctorReviewRepository;

        public DoctorReviewService(IDoctorReviewRepository doctorReviewRepository)
        {
            if (doctorReviewRepository == null)
                throw new ArgumentNullException(nameof(doctorReviewRepository));
            _doctorReviewRepository = doctorReviewRepository;
        }

        public IEnumerable<DoctorReview> GetAllReviews()
        {
            return _doctorReviewRepository.GetAllDoctorReviews().ToList();
        }

        public IEnumerable<DoctorReview> GetReviewsByDoctorId(int id)
        {
            return _doctorReviewRepository.GetReviewsByDoctorId(id).ToList();
        }

        public DtoPagedDoctorReview GetPagedReviewByDoctorId(int id, int perPage, int numPage)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var reviews = _doctorReviewRepository.GetReviewsByDoctorId(id);
            var pagedList = new PagedList<DoctorReview>(reviews, perPage, numPage);

            if (!pagedList.Any())
                throw new ApplicationException("Page not found");

            var result = new DtoPagedDoctorReview()
            {
                DoctorReviews = pagedList.ToList(),
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }

        public DoctorReview GetReviewById(int id)
        {
            return _doctorReviewRepository.GetDoctorReviewById(id);
        }

        public void Add(DoctorReview dr)
        {
            if (dr.UserId == null || dr.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            if (dr.Text == "" || dr.DoctorId == 0 || dr.RatePoliteness == 0 ||
                dr.RateProfessionalism == 0 || dr.RateWaitingTime == 0 || dr.Text == string.Empty)
                throw new ApplicationException("Incorrect data, some required fields are null or empty");

            _doctorReviewRepository.Add(dr);
        }

        public void Update(DoctorReview dr)
        {
            if (dr.UserId == null || dr.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            if (dr.Text == "" || dr.DoctorId == 0 || dr.RatePoliteness == 0 ||
                dr.RateProfessionalism == 0 || dr.RateWaitingTime == 0 || dr.Text == string.Empty)
                throw new ApplicationException("Incorrect data, some required fields are null or empty");

            _doctorReviewRepository.Update(dr);
        }

        public void Delete(int id)
        {
            DoctorReview deleted;

            try
            {
                deleted = _doctorReviewRepository.GetDoctorReviewById(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Review not found");
            }

            if (deleted != null)
                _doctorReviewRepository.Delete(deleted);
        }
    }
}
