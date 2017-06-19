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
    public class DoctorReviewService : IDoctorReviewService
    {
        private readonly IDoctorReviewRepository _doctorReviewRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILanguageRepository _languageRepository;

        public DoctorReviewService(IDoctorReviewRepository doctorReviewRepository, IDoctorRepository doctorRepository, ILanguageRepository languageRepository)
        {
            if (doctorReviewRepository == null)
                throw new ArgumentNullException(nameof(doctorReviewRepository));
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));
            _doctorReviewRepository = doctorReviewRepository;
            _doctorRepository = doctorRepository;
            _languageRepository = languageRepository;
        }

        public IEnumerable<DtoDoctorReview> GetAllReviews()
        {
            var doctorReviewsList = _doctorReviewRepository.GetAllDoctorReviews().ToList();
            var dtoDoctorReviewsList = new List<DtoDoctorReview>();
            foreach (var doctorReview in doctorReviewsList)
            {
                dtoDoctorReviewsList.Add(new DtoDoctorReview()
                {
                    Id = doctorReview.Id,
                    Text = doctorReview.Text,
                    UserName = doctorReview.UserName,
                    UserId = doctorReview.UserId,
                    PublishTime = doctorReview.PublishTime,
                    RatePoliteness = doctorReview.RatePoliteness,
                    RateWaitingTime = doctorReview.RateWaitingTime,
                    RateProfessionalism = doctorReview.RateProfessionalism,
                    DoctorId = doctorReview.Doctor.Id,
                    LangCode = doctorReview.Language.Code
                });
            }
            return dtoDoctorReviewsList;
        }

        public IEnumerable<DtoDoctorReview> GetReviewsByDoctorId(int id)
        {
            var doctorReviewsList = _doctorReviewRepository.GetReviewsByDoctorId(id).ToList();
            var dtoDoctorReviewsList = new List<DtoDoctorReview>();
            foreach (var doctorReview in doctorReviewsList)
            {
                dtoDoctorReviewsList.Add(new DtoDoctorReview()
                {
                    Id = doctorReview.Id,
                    Text = doctorReview.Text,
                    UserName = doctorReview.UserName,
                    UserId = doctorReview.UserId,
                    PublishTime = doctorReview.PublishTime,
                    RatePoliteness = doctorReview.RatePoliteness,
                    RateWaitingTime = doctorReview.RateWaitingTime,
                    RateProfessionalism = doctorReview.RateProfessionalism,
                    DoctorId = doctorReview.Doctor.Id,
                    LangCode = doctorReview.Language.Code
                });
            }
            return dtoDoctorReviewsList;
        }

        public DtoPagedDoctorReview GetPagedReviewByDoctorId(int id, int perPage, int numPage)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var reviews = _doctorReviewRepository.GetReviewsByDoctorId(id);
            var pagedList = new PagedList<DoctorReview>(reviews, perPage, numPage);

            if (!pagedList.Any())
                throw new ApplicationException("Page not found");
            var dtoDoctorReviewsList = new List<DtoDoctorReview>();
            foreach (var doctorReview in pagedList)
            {
                dtoDoctorReviewsList.Add(new DtoDoctorReview()
                {
                    Id = doctorReview.Id,
                    Text = doctorReview.Text,
                    UserName = doctorReview.UserName,
                    UserId = doctorReview.UserId,
                    PublishTime = doctorReview.PublishTime,
                    RatePoliteness = doctorReview.RatePoliteness,
                    RateWaitingTime = doctorReview.RateWaitingTime,
                    RateProfessionalism = doctorReview.RateProfessionalism,
                    DoctorId = doctorReview.Doctor.Id,
                    LangCode = doctorReview.Language.Code
                });
            }
            var result = new DtoPagedDoctorReview()
            {
                DoctorReviews = dtoDoctorReviewsList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }

        public DtoDoctorReview GetReviewById(int id)
        {
            DoctorReview doctorReview;
            try
            {
                doctorReview = _doctorReviewRepository.GetDoctorReviewById(id);
            }
            catch
            {
                throw new ApplicationException("DoctorReview not found");
            }
            var dtoDoctorReview  = new DtoDoctorReview()
            {
                Id = doctorReview.Id,
                Text = doctorReview.Text,
                UserName = doctorReview.UserName,
                UserId = doctorReview.UserId,
                PublishTime = doctorReview.PublishTime,
                RatePoliteness = doctorReview.RatePoliteness,
                RateWaitingTime = doctorReview.RateWaitingTime,
                RateProfessionalism = doctorReview.RateProfessionalism,
                DoctorId = doctorReview.Doctor.Id,
                LangCode = doctorReview.Language.Code
            };
            return dtoDoctorReview;
        }

        public void Add(DtoDoctorReview doctorReview)
        {
            if (doctorReview.UserId == null || doctorReview.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            Doctor doctor;

            try
            {
                doctor = _doctorRepository.GetDoctorById(doctorReview.DoctorId);
            }
            catch
            {
                throw new ApplicationException("Doctor not found");
            }

            if (doctorReview.Text == "" || doctorReview.DoctorId < 0 || doctorReview.RatePoliteness < 0 ||
                doctorReview.RateProfessionalism < 0 || doctorReview.RateWaitingTime < 0 || doctorReview.Text == string.Empty)
                throw new ApplicationException("Incorrect data, some required fields are null or empty");

            var lang = _languageRepository.GetLanguageByCode(doctorReview.LangCode);

            var newDoctorReview = new DoctorReview()
            {
                Id = doctorReview.Id,
                Text = doctorReview.Text,
                UserName = doctorReview.UserName,
                UserId = doctorReview.UserId,
                PublishTime = doctorReview.PublishTime,
                RatePoliteness = doctorReview.RatePoliteness,
                RateWaitingTime = doctorReview.RateWaitingTime,
                RateProfessionalism = doctorReview.RateProfessionalism,
                Doctor = doctor,
                IsApproved = true, //TODO: change to false when moderator control will be done
                Language = lang
            };

            _doctorReviewRepository.Add(newDoctorReview);
        }

        public void Update(DtoDoctorReview doctorReview)
        {
            if (doctorReview.UserId == null || doctorReview.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            if (doctorReview.Text == "" || doctorReview.DoctorId < 0 || doctorReview.RatePoliteness < 0 ||
                doctorReview.RateProfessionalism < 0 || doctorReview.RateWaitingTime < 0 || doctorReview.Text == string.Empty)
                throw new ApplicationException("Incorrect data, some required fields are null or empty");

            DoctorReview updatedDoctorReview;

            try
            {
                updatedDoctorReview = _doctorReviewRepository.GetDoctorReviewById(doctorReview.Id);
            }
            catch
            {
                throw new ApplicationException("Review not found");
            }

            if (updatedDoctorReview == null) return;

            Doctor doctor;

            try
            {
                doctor = _doctorRepository.GetDoctorById(doctorReview.DoctorId);
            }
            catch
            {
                throw new ApplicationException("Doctor not found");
            }

            var lang = _languageRepository.GetLanguageByCode(doctorReview.LangCode);

            updatedDoctorReview.Id = doctorReview.Id;
            updatedDoctorReview.Text = doctorReview.Text;
            updatedDoctorReview.UserName = doctorReview.UserName;
            updatedDoctorReview.UserId = doctorReview.UserId;
            updatedDoctorReview.PublishTime = doctorReview.PublishTime;
            updatedDoctorReview.RatePoliteness = doctorReview.RatePoliteness;
            updatedDoctorReview.RateWaitingTime = doctorReview.RateWaitingTime;
            updatedDoctorReview.RateProfessionalism = doctorReview.RateProfessionalism;
            updatedDoctorReview.Doctor = doctor;
            updatedDoctorReview.IsApproved = true; //TODO: change to false when moderator control will be done
            updatedDoctorReview.Language = lang;

            _doctorReviewRepository.Update(updatedDoctorReview);
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
