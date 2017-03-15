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
    public class ClinicService : IClinicService
    {
        private readonly IСlinicRepository _clinicRepository;
        private readonly IClinicReviewRepository _clinicReviewRepository;

        public ClinicService(IСlinicRepository clinicRepository, IClinicReviewRepository clinicReviewRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (clinicReviewRepository == null)
                throw new ArgumentNullException(nameof(clinicReviewRepository));
            _clinicReviewRepository = clinicReviewRepository;
            _clinicRepository = clinicRepository;
        }

        public IEnumerable<DtoClinic> GetAllClinics(string pathToImage)
        {
            var clinicList = _clinicRepository.GetAllСlinics().ToList();

            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in clinicList)
            {
                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Email = clinic.Email,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Image = pathToImage + clinic.ImageName
                };
                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicAddress = new DtoAddress
                    {
                        City = clinicAddress.City.Name,
                        Street = clinicAddress.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };
                    foreach (var clinicPhone in clinicAddress.ClinicPhones)
                    {
                        var dtoClinicPhone = new DtoPhone() { Desc = clinicPhone.Description, Phone = clinicPhone.Number };
                        dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                    }
                    dtoClinicAddressList.Add(dtoClinicAddress);
                }
                //foreach (var clinicAddress in clinic.ClinicAddresses)
                //{
                //    var dtoClinicAddress = new DtoClinicAddress
                //    {
                //        ClinicAddress = clinicAddress.Address,
                //        ClinicPhone = new List<Dictionary<string, string>>()
                //    };
                //    foreach (var dtoClinicPhone in clinicAddress.ClinicPhones.Select(clinicPhone => new Dictionary<string, string> { { clinicPhone.Description, clinicPhone.Number } }))
                //    {
                //        dtoClinicAddress.ClinicPhone.Add(dtoClinicPhone);
                //    }
                //    dtoClinicAddressList.Add(dtoClinicAddress);
                //}
                dtoClinic.ClinicAddress = dtoClinicAddressList;

                var dtoSpecializationList = clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
                dtoClinic.ClinicSpecialization = dtoSpecializationList;

                //var reviews = _clinicReviewRepository.GetReviewsByClinicId(clinic.Id).ToList();
                //double ratePrice = 0;
                //double rateQuality = 0;
                //double ratePoliteness = 0;

                //foreach (var review in reviews)
                //{
                //    ratePrice += review.RatePrice;
                //    rateQuality += review.RateQuality;
                //    ratePoliteness += review.RatePoliteness;
                //}

                //if (reviews.Count != 0)
                //{
                //    ratePrice /= reviews.Count;
                //    rateQuality /= reviews.Count;
                //    ratePoliteness /= reviews.Count;
                //}

                //dtoClinic.RatePrice = ratePrice;
                //dtoClinic.RateQuality = rateQuality;
                //dtoClinic.RatePoliteness = ratePoliteness;

                dtoClinicList.Add(dtoClinic);
            }
            return dtoClinicList;
        }

        public DtoClinic GetClinicById(int id, string pathToImage)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }
            var dtoClinic = new DtoClinic
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Email = clinic.Email,
                RatePoliteness = clinic.RatePoliteness,
                RatePrice = clinic.RatePrice,
                RateQuality = clinic.RateQuality,
                RateAverage = clinic.RateAverage,
                Image = pathToImage + clinic.ImageName
            };
            var dtoClinicAddressList = new List<DtoAddress>();
            foreach (var clinicAddress in clinic.CityAddresses)
            {
                var dtoClinicAddress = new DtoAddress
                {
                    City = clinicAddress.City.Name,
                    Street = clinicAddress.Street,
                    ClinicPhones = new List<DtoPhone>()
                };
                foreach (var dtoClinicPhone in clinicAddress.ClinicPhones.Select(clinicPhone => new DtoPhone() { Desc = clinicPhone.Description, Phone = clinicPhone.Number }))
                {
                    dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                }
                dtoClinicAddressList.Add(dtoClinicAddress);
            }
            dtoClinic.ClinicAddress = dtoClinicAddressList;

            var dtoSpecializationList = clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
            dtoClinic.ClinicSpecialization = dtoSpecializationList;


            //var reviews = _clinicReviewRepository.GetReviewsByClinicId(clinic.Id).ToList();
            //double ratePrice = 0;
            //double rateQuality = 0;
            //double ratePoliteness = 0;

            //foreach (var review in reviews)
            //{
            //    ratePrice += review.RatePrice;
            //    rateQuality += review.RateQuality;
            //    ratePoliteness += review.RatePoliteness;
            //}

            //if (reviews.Count != 0)
            //{
            //    ratePrice /= reviews.Count;
            //    rateQuality /= reviews.Count;
            //    ratePoliteness /= reviews.Count;
            //}

            //dtoClinic.RatePrice = ratePrice;
            //dtoClinic.RateQuality = rateQuality;
            //dtoClinic.RatePoliteness = ratePoliteness;

            return dtoClinic;
        }

        public DtoPagedClinic GetPagedClinics(int perPage, int numPage, string pathToImage)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            var clinics = _clinicRepository.GetAllСlinics();
            var pagedList = new PagedList<Clinic>(clinics, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }
            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in pagedList)
            {
                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Email = clinic.Email,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Image = pathToImage + clinic.ImageName
                };
                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicAddress = new DtoAddress
                    {
                        City = clinicAddress.City.Name,
                        Street = clinicAddress.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };
                    foreach (var dtoClinicPhone in clinicAddress.ClinicPhones.Select(clinicPhone => new DtoPhone()
                    {
                        Desc = clinicPhone.Description,
                        Phone = clinicPhone.Number
                    }))
                    {
                        dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                    }
                    dtoClinicAddressList.Add(dtoClinicAddress);
                }
                dtoClinic.ClinicAddress = dtoClinicAddressList;

                var dtoSpecializationList =
                    clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
                dtoClinic.ClinicSpecialization = dtoSpecializationList;

                //var reviews = _clinicReviewRepository.GetReviewsByClinicId(clinic.Id).ToList();
                //double ratePrice = 0;
                //double rateQuality = 0;
                //double ratePoliteness = 0;

                //foreach (var review in reviews)
                //{
                //    ratePrice += review.RatePrice;
                //    rateQuality += review.RateQuality;
                //    ratePoliteness += review.RatePoliteness;
                //}

                //if (reviews.Count != 0)
                //{
                //    ratePrice /= reviews.Count;
                //    rateQuality /= reviews.Count;
                //    ratePoliteness /= reviews.Count;
                //}

                //dtoClinic.RatePrice = ratePrice;
                //dtoClinic.RateQuality = rateQuality;
                //dtoClinic.RatePoliteness = ratePoliteness;

                dtoClinicList.Add(dtoClinic);
            }

            var pagedDtoClinicList = new DtoPagedClinic
            {
                Clinics = dtoClinicList,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };
            return pagedDtoClinicList;
        }

        public DtoPagedClinic SearchClinics(int perPage, int numPage, DtoClinicSearchModel searchModel, string pathToImage)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            bool descending;
            try
            {
                descending = Convert.ToBoolean(searchModel.Descending);
            }
            catch (Exception)
            {
                throw new ApplicationException("Incorrect request parameter"); ;
            }
            IQueryable<Clinic> clinics;
            if (searchModel.CityId == 0)
            {
                if (searchModel.SpecializationIds.Any())
                {
                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                        Where(x => (x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                        !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any()));
                    //var clinicList = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending);
                    //var searchResult = (from clinic in clinicList let isSubset = !searchModel.SpecializationId.Except(clinic.ClinicSpecializations.Select(x => x.Id)).Any() where isSubset select clinic);
                    //var qqq = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                    //    Where(x => x.Name.Contains(searchModel.SearchWord) &&
                    //    !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any()).ToList();
                    //x.ClinicSpecializations.Any(z => z.Name.Contains(searchModel.SearchWord.ToLowerInvariant())
                }
                else
                {
                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                        Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()));
                }
            }
            else
            {
                if (searchModel.SpecializationIds.Any())
                {
                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                        Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                   x.CityAddresses.Any(y => y.City.Id == searchModel.CityId) &&
                                   !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any());
                }
                else
                {
                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                        Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                        x.CityAddresses.Any(y => y.City.Id == searchModel.CityId));
                }
            }

            var pagedList = new PagedList<Clinic>(clinics, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }
            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in pagedList)
            {
                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Email = clinic.Email,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Image = pathToImage + clinic.ImageName
                };
                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicAddress = new DtoAddress
                    {
                        City = clinicAddress.City.Name,
                        Street = clinicAddress.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };
                    foreach (var dtoClinicPhone in clinicAddress.ClinicPhones.Select(clinicPhone => new DtoPhone()
                    {
                        Desc = clinicPhone.Description,
                        Phone = clinicPhone.Number
                    }))
                    {
                        dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                    }
                    dtoClinicAddressList.Add(dtoClinicAddress);
                }
                dtoClinic.ClinicAddress = dtoClinicAddressList;

                var dtoSpecializationList =
                    clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
                dtoClinic.ClinicSpecialization = dtoSpecializationList;

                dtoClinicList.Add(dtoClinic);
            }
            var pagedDtoClinicList = new DtoPagedClinic
            {
                Clinics = dtoClinicList,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };
            return pagedDtoClinicList;
        }
        public void Add(Clinic clinic)
        {
            if (clinic == null)
                throw new ArgumentNullException(nameof(clinic));

            var c = clinic;

            _clinicRepository.Add(c);
        }


        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var c = _clinicRepository.GetClinicById(id);
            if (c != null)
            {
                c.Name = name;

                _clinicRepository.Update(c);
            }
        }

        public void Delete(int id)
        {
            var cp = _clinicRepository.GetClinicById(id);

            if (cp != null)
                _clinicRepository.Delete(cp);
        }
    }
}
