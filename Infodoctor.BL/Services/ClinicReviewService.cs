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
    public class ClinicReviewService : IClinicReviewService
    {
        private readonly IClinicReviewRepository _clinicReviewRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ILanguageRepository _languageRepository;

       public ClinicReviewService(IClinicReviewRepository clinicReviewRepository, IClinicRepository clinicRepository, ILanguageRepository languageRepository)
        {
            if (clinicReviewRepository == null)
                throw new ArgumentNullException(nameof(clinicReviewRepository));
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));
            _clinicReviewRepository = clinicReviewRepository;
            _clinicRepository = clinicRepository;
            _languageRepository = languageRepository;
        }

        public IEnumerable<DtoClinicReview> GetClinicReviews()
        {
            var clinicReviewsList = _clinicReviewRepository.GetAllClinicReviews().ToList();
            var dtoClinicReviewsList = new List<DtoClinicReview>();
            foreach (var clinicReview in clinicReviewsList)
            {
                dtoClinicReviewsList.Add(new DtoClinicReview()
                {
                    Id = clinicReview.Id,
                    Text = clinicReview.Text,
                    UserName = clinicReview.UserName,
                    UserId = clinicReview.UserId,
                    PublishTime = clinicReview.PublishTime,
                    RatePoliteness = clinicReview.RatePoliteness,
                    RatePrice = clinicReview.RatePrice,
                    RateQuality = clinicReview.RateQuality,
                    ClinicId = clinicReview.Clinic.Id,
                    LangCode = clinicReview.Language.Code.ToLower()
                });
            }
            return dtoClinicReviewsList;
        }

        public IEnumerable<DtoClinicReview> GetReviewsByClinicId(int id)
        {
            var clinicReviewsList = _clinicReviewRepository.GetReviewsByClinicId(id).ToList();
            var dtoClinicReviewsList = new List<DtoClinicReview>();
            foreach (var clinicReview in clinicReviewsList)
            {
                dtoClinicReviewsList.Add(new DtoClinicReview()
                {
                    Id = clinicReview.Id,
                    Text = clinicReview.Text,
                    UserName = clinicReview.UserName,
                    UserId = clinicReview.UserId,
                    PublishTime = clinicReview.PublishTime,
                    RatePoliteness = clinicReview.RatePoliteness,
                    RatePrice = clinicReview.RatePrice,
                    RateQuality = clinicReview.RateQuality,
                    ClinicId = clinicReview.Clinic.Id,
                    LangCode = clinicReview.Language.Code.ToLower()
                });
            }
            return dtoClinicReviewsList;
        }

        public DtoPagedClinicReview GetPagedReviewsByClinicId(int id, int perPage, int numPage)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            var reviews = _clinicReviewRepository.GetReviewsByClinicId(id).Where(x => x.IsApproved);
            var pagedList = new PagedList<ClinicReview>(reviews, perPage, numPage);
            if (!pagedList.Any())
            {
                throw new ApplicationException("Page not found");
            }
            var dtoClinicReviewsList = new List<DtoClinicReview>();
            foreach (var clinicReview in pagedList)
            {
                dtoClinicReviewsList.Add(new DtoClinicReview()
                {
                    Id = clinicReview.Id,
                    Text = clinicReview.Text,
                    UserName = clinicReview.UserName,
                    UserId = clinicReview.UserId,
                    PublishTime = clinicReview.PublishTime,
                    RatePoliteness = clinicReview.RatePoliteness,
                    RatePrice = clinicReview.RatePrice,
                    RateQuality = clinicReview.RateQuality,
                    ClinicId = clinicReview.Clinic.Id,
                    LangCode = clinicReview.Language.Code.ToLower()
                });
            }
            var result = new DtoPagedClinicReview
            {
                ClinicReviews = dtoClinicReviewsList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }

        public DtoPagedClinicReview GetPagedReviewsByClinicId(int id, int perPage, int numPage, string lang)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            lang = lang.ToLower();

            var reviews = _clinicReviewRepository.GetReviewsByClinicId(id).Where(x => x.IsApproved)
                .OrderByDescending(c => c.Language.Code.ToLower() == lang)
                .ThenByDescending(c => c.Language.Code.ToLower() != lang)
                .ThenByDescending(c => c.PublishTime);

            var pagedList = new PagedList<ClinicReview>(reviews, perPage, numPage);

            if (!pagedList.Any())
                throw new ApplicationException("Page not found");

            var dtoClinicReviewsList = new List<DtoClinicReview>();
            foreach (var clinicReview in pagedList)
            {
                dtoClinicReviewsList.Add(new DtoClinicReview()
                {
                    Id = clinicReview.Id,
                    Text = clinicReview.Text,
                    UserName = clinicReview.UserName,
                    UserId = clinicReview.UserId,
                    PublishTime = clinicReview.PublishTime,
                    RatePoliteness = clinicReview.RatePoliteness,
                    RatePrice = clinicReview.RatePrice,
                    RateQuality = clinicReview.RateQuality,
                    ClinicId = clinicReview.Clinic.Id,
                    LangCode = clinicReview.Language.Code.ToLower()
                });
            }
            var result = new DtoPagedClinicReview
            {
                ClinicReviews = dtoClinicReviewsList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };
            return result;
        }

        public DtoClinicReview GetClinicReviewById(int id)
        {
            ClinicReview clinicReview;
            try
            {
                clinicReview = _clinicReviewRepository.GetClinicReviewById(id);
            }
            catch
            {
                throw new ApplicationException("ClinicReview not found");
            }
            var dtoClinicReview = new DtoClinicReview()
            {
                Id = clinicReview.Id,
                Text = clinicReview.Text,
                UserName = clinicReview.UserName,
                UserId = clinicReview.UserId,
                PublishTime = clinicReview.PublishTime,
                RatePoliteness = clinicReview.RatePoliteness,
                RatePrice = clinicReview.RatePrice,
                RateQuality = clinicReview.RateQuality,
                ClinicId = clinicReview.Clinic.Id,
                LangCode = clinicReview.Language.Code.ToLower()
            };

            return dtoClinicReview;
        }

        public void Add(DtoClinicReview clinicReview)
        {
            if (clinicReview.UserId == null || clinicReview.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            Clinic clinic;

            try
            {
                clinic = _clinicRepository.GetClinic(clinicReview.ClinicId);
            }
            catch 
            {
                throw new ApplicationException("Clinic not found");
            }

            if (clinicReview.Text == "" || clinicReview.ClinicId == 0 || clinicReview.RatePoliteness < 1 ||
                clinicReview.RatePrice < 1 || clinicReview.RateQuality < 1  || clinicReview.Text == string.Empty)
                throw new ApplicationException("Incorrect data, pussible some required fields are null or empty");

            var lang = _languageRepository.GetLanguageByCode(clinicReview.LangCode);

            var newClinicReview = new ClinicReview()
            {
                Text = clinicReview.Text,
                UserName = clinicReview.UserName,
                UserId = clinicReview.UserId,
                PublishTime = clinicReview.PublishTime,
                RatePoliteness = clinicReview.RatePoliteness,
                RatePrice = clinicReview.RatePrice,
                RateQuality = clinicReview.RateQuality,
                Clinic = clinic,
                IsApproved = true, //TODO: change to false when moderator control will be done
                Language = lang
            };
            _clinicReviewRepository.Add(newClinicReview);
        }

        public void Delete(int id)
        {
            ClinicReview clinicReview;
            try
            {
                clinicReview = _clinicReviewRepository.GetClinicReviewById(id);
            }
            catch
            {
                throw new ApplicationException("Review not found");
            }
            if (clinicReview != null)
                _clinicReviewRepository.Delete(clinicReview);
        }

        public void Update(ClinicReview clinicReview)
        {
            throw new NotImplementedException();
        }
    }
}
