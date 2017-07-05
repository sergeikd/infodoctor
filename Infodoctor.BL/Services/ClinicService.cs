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

        public DtoClinicMultiLang GetClinicById(int id, string pathToClinicImage)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }

            var imagesList = clinic.ImageName.Select(image => pathToClinicImage + image.Name).ToList();

            var dtoAddreses = new List<DtoAddressMultiLang>();
            foreach (var address in clinic.Addresses)
            {
                var coords = new Coords()
                {
                    Lat = address.Lat,
                    Lng = address.Lng
                };

                var localAddresses = new List<LocalizedDtoAddress>();
                foreach (var local in address.LocalizedAddresses)
                {
                    var localAddress = new LocalizedDtoAddress()
                    {
                        Id = local.Id,
                        Country = address.Country.LocalizedCountries?.First(l => l.Language.Code.ToLower() == local.Language.Code.ToLower())?.Name,
                        City = address.City.LocalizedCities?.First(l => l.Language.Code.ToLower() == local.Language.Code.ToLower())?.Name,
                        Street = local.Street,
                        LangCode = local.Language?.Code.ToLower()
                    };
                    localAddresses.Add(localAddress);
                }

                var dtoPhones = new List<DtoPhoneMultiLang>();
                foreach (var phone in address.Phones)
                {
                    var localPhones = new List<LocalizedDtoPhone>();

                    if (phone.LocalizedPhones != null)
                        foreach (var local in phone.LocalizedPhones)
                        {
                            var localPhone = new LocalizedDtoPhone()
                            {
                                Id = local.Id,
                                Desc = local.Description,
                                Number = local.Number,
                                LangCode = local.Language?.Code.ToLower()
                            };
                            localPhones.Add(localPhone);
                        }

                    dtoPhones.Add(new DtoPhoneMultiLang()
                    {
                        Id = phone.Id,
                        LocalizedPhone = localPhones
                    });
                }

                var dtoAddress = new DtoAddressMultiLang()
                {
                    Id = address.Id,
                    Coords = coords,
                    Phones = dtoPhones,
                    LocalizedAddress = localAddresses
                };

                dtoAddreses.Add(dtoAddress);
            }

            var localClinics = new List<LocalizedDtoClinic>();
            foreach (var local in clinic.Localized)
            {
                var localClinic = new LocalizedDtoClinic()
                {
                    Id = local.Id,
                    Name = local.Name,
                    Specializations = local.Specializations,
                    LangCode = local.Language?.Code.ToLower()
                };
                localClinics.Add(localClinic);
            }

            var doctorsId = clinic.Doctors.Select(doctor => doctor.Id).ToList();

            var dtoClinic = new DtoClinicMultiLang()
            {
                Id = clinic.Id,
                Images = imagesList,
                Email = clinic.Email,
                Site = clinic.Site,
                RatePrice = clinic.RatePrice,
                RateQuality = clinic.RateQuality,
                RatePoliteness = clinic.RatePoliteness,
                RateAverage = clinic.RateAverage,
                ReviewCount = clinic.ClinicReviews.Count,
                Favorite = clinic.Favorite,
                ClinicAddress = dtoAddreses,
                DoctorsIdList = doctorsId,
                LocalizedClinic = localClinics
            };

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
                                            x => x.Addresses.Any(y => y.City.Id == searchModel.CityId)
                                        );
                                    break;
                                }
                            default:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                        Where(
                                            x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                            x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Specializations.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                            x.Addresses.Any(y => y.City.Id == searchModel.CityId)
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
                if (clinicAddress.City != null)
                    foreach (var city in clinicAddress.City.LocalizedCities)
                        if (string.Equals(city.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedCity = city;


                var localizedCountry = new LocalizedCountry();
                if (clinicAddress.City != null)
                    foreach (var country in clinicAddress.Country.LocalizedCountries)
                        if (string.Equals(country.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedCountry = country;

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
                    Country = localizedCountry.Name,
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
                DoctorsIdList = doctors
            };

            return dtoClinic;
        }
    }
}