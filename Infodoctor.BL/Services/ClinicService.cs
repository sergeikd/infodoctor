﻿using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly ISearchService _searchService;

        public ClinicService(IClinicRepository clinicRepository, ISearchService searchService)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));

            _searchService = searchService;
            _clinicRepository = clinicRepository;
        }

        public IEnumerable<DtoClinicSingleLang> GetAllClinics(string pathToClinicImage, string lang)
        {
            var clinicList = _clinicRepository.GetAllСlinics().ToList();
            var dtoClinicList = new List<DtoClinicSingleLang>();

            foreach (var clinic in clinicList)
            {
                var dtoClinic = ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic);
                dtoClinicList.Add(dtoClinic);
            }

            return dtoClinicList;
        }

        public DtoClinicSingleLang GetClinicById(int id, string pathToClinicImage, string lang)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }

            var dtoClinic = ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic);

            return dtoClinic;
        }

        public DtoPagedClinic GetPagedClinics(int perPage, int numPage, string pathToClinicImage, string lang)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            var clinics = _clinicRepository.GetAllСlinics();
            var pagedList = new PagedList<Domain.Entities.Clinic>(clinics, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }
            var dtoClinicList = new List<DtoClinicSingleLang>();
            foreach (var clinic in pagedList)
            {
                var dtoClinic = ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic);
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

        public DtoPagedClinic SearchClinics(int perPage, int numPage, DtoClinicSearchModel searchModel,
            string pathToClinicImage, string lang)
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
                throw new ApplicationException("Incorrect request parameter");
            }

            IQueryable<Clinic> clinics;
            switch (searchModel.CityId) //check whether CityId included in search request
            {
                case 0:
                    {
                        switch (searchModel.SearchWord == "")
                        {
                            case true:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang);
                                    break;
                                }
                            default:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                        Where(
                                            x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                            x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Specializations.ToLower().Contains(searchModel.SearchWord.ToLower())
                                        );
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        switch (searchModel.SearchWord == "")
                        {
                            case true:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                        Where(
                                            x => x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId)
                                        );
                                    break;
                                }
                            default:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                        Where(
                                            x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                            x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Specializations.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                            x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId)
                                    );
                                    break;
                                }
                        }
                        break;
                    }
            }

            var pagedList = new PagedList<Clinic>(clinics, perPage, numPage);

            if (!pagedList.Any())
            {
                return null;
            }

            var dtoClinicList = new List<DtoClinicSingleLang>();

            foreach (var clinic in pagedList)
            {
                var dtoClinic = ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic);
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

        public void Add(DtoClinicMultiLang clinic)
        {
            throw new NotImplementedException();
            //if (clinic == null)
            //    throw new ArgumentNullException(nameof(clinic));

            //var c = clinic;

            //_clinicRepository.Add(c);
            //_searchService.RefreshCache();
        }

        public void Update(DtoClinicMultiLang clinic)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name));

            //var c = _clinicRepository.GetClinicById(id);
            //if (c != null)
            //{
            //    c.Name = name;
            //    _clinicRepository.Update(c);
            //    _searchService.RefreshCache();
            //}
        }

        public void Delete(int id)
        {
            var cp = _clinicRepository.GetClinicById(id);

            if (cp == null) return;
            _clinicRepository.Delete(cp);
            _searchService.RefreshCache();
        }

        private static DtoClinicSingleLang ConvertEntityToDtoSingleLang(string lang, string pathToClinicImage, Clinic clinic)
        {
            if (clinic == null)
                throw new ApplicationException("Clinic not found");

            var imagesList = clinic.ImageName.Select(image => pathToClinicImage + image.Name).ToList();

            var dtoAddreses = new List<DtoAddressSingleLang>();
            foreach (var clinicAddress in clinic.Addresses)
            {
                var coords = new Coords()
                {
                    Lat = clinicAddress.Lat,
                    Lng = clinicAddress.Lng
                };
                var localizedAddress = new LocalizedAddress();
                foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses.ToList())
                    if (string.Equals(clinicAddressLocalizedAddress.Language.Code.ToLower(), lang.ToLower(),
                        StringComparison.Ordinal))
                        localizedAddress = clinicAddressLocalizedAddress;

                var localizedCity = new LocalizedCity();
                if (localizedAddress.City != null)
                    foreach (var cityLocalizedCity in localizedAddress.City.LocalizedCities)
                    {
                        if (string.Equals(cityLocalizedCity.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedCity = cityLocalizedCity;
                    }

                var phones = (from clinicPhone in clinicAddress.Phones
                              from localizedPhone in clinicPhone.LocalizedPhones
                              where string.Equals(localizedPhone.Language.Code.ToLower(), lang.ToLower(), StringComparison.Ordinal)
                              select new DtoPhoneSingleLang()
                              {
                                  Id = localizedPhone.Id,
                                  Desc = localizedPhone.Description,
                                  Number = localizedPhone.Number
                              }).ToList();

                dtoAddreses.Add(new DtoAddressSingleLang()
                {
                    Id = clinicAddress.Id,
                    Country = localizedAddress.Country,
                    City = localizedCity.Name,
                    Street = localizedAddress.Street,
                    Phones = phones,
                    Coords = coords
                });
            }

            var localizedClinic = new LocalizedClinic();
            foreach (var localClinic in clinic.Localized)
                if (localClinic.Language.Code.ToLower() == lang.ToLower())
                    localizedClinic = localClinic;

            var doctors = clinic.Doctors.Select(doctor => doctor.Id).ToList();

            var dtoClinic = new DtoClinicSingleLang()
            {
                LangCode = localizedClinic.Language?.Code.ToLower(),
                Id = clinic.Id,
                Name = localizedClinic.Name,
                Email = clinic.Email,
                Site = clinic.Site,
                RatePoliteness = clinic.RatePoliteness,
                RateQuality = clinic.RateQuality,
                RatePrice = clinic.RatePrice,
                RateAverage = clinic.RateAverage,
                ReviewCount = clinic.ClinicReviews.Count,
                Favorite = clinic.Favorite,
                Images = imagesList,
                Addresses = dtoAddreses,
                Specializations = localizedClinic.Specializations,
                DoctorsId = doctors
            };

            return dtoClinic;
        }
    }
}