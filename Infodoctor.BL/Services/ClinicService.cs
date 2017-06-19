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
        private readonly IClinicReviewRepository _clinicReviewRepository;
        private readonly ISearchService _searchService;
        private readonly IImagesService _imagesService;

        public ClinicService(IClinicRepository clinicRepository, IClinicReviewRepository clinicReviewRepository,
            ISearchService searchService, IImagesService imagesService)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (clinicReviewRepository == null)
                throw new ArgumentNullException(nameof(clinicReviewRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));
            if (imagesService == null)
                throw new ArgumentNullException(nameof(imagesService));

            _searchService = searchService;
            _clinicReviewRepository = clinicReviewRepository;
            _clinicRepository = clinicRepository;
            _imagesService = imagesService;
        }

        public IEnumerable<DtoClinicSingleLang> GetAllClinics(string pathToImage, string lang)
        {
            var clinicList = _clinicRepository.GetAllСlinics().ToList();
            var dtoClinicList = new List<DtoClinicSingleLang>();

            foreach (var clinic in clinicList)
            {
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

                var dtoAddreses = new List<DtoAddressSingleLang>();
                foreach (var clinicAddress in clinic.Addresses)
                {
                    var localizedAddress = new LocalizedAddress();
                    foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses.ToList())
                    {
                        if (string.Equals(clinicAddressLocalizedAddress.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedAddress = clinicAddressLocalizedAddress;
                    }

                    var localizedCity = new LocalizedCity();
                    if (localizedAddress.City?.LocalizedCities != null)
                        foreach (var cityLocalizedCity in localizedAddress.City?.LocalizedCities)
                        {
                            if (string.Equals(cityLocalizedCity.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                                localizedCity = cityLocalizedCity;
                        }

                    var phones = new List<DtoPhoneSingleLang>();
                    foreach (var clinicPhone in clinicAddress.Phones)
                        foreach (var localizedPhone in clinicPhone.LocalizedPhones)
                            if (string.Equals(localizedPhone.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                                phones.Add(new DtoPhoneSingleLang()
                                {
                                    Id = localizedPhone.Id,
                                    Desc = localizedPhone.Description,
                                    Number = localizedPhone.Number
                                });

                    dtoAddreses.Add(new DtoAddressSingleLang()
                    {
                        Id = clinicAddress.Id,
                        Country = localizedAddress.Country,
                        City = localizedCity.Name,
                        Street = localizedAddress.Street,
                        Phones = phones
                    });
                }

                var dtoSpecializations = new List<DtoClinicSpecializationSingleLangModels>();
                foreach (var clinicSpecialization in clinic.ClinicSpecializations)
                {
                    var localizedCs = new LocalizedClinicSpecialization(); ;
                    foreach (var localizedSpecialization in clinicSpecialization.LocalizedClinicSpecializations)
                    {
                        if (localizedSpecialization.Language.Code.ToLower() == lang.ToLower())
                            localizedCs = localizedSpecialization;
                    }

                    dtoSpecializations.Add(new DtoClinicSpecializationSingleLangModels()
                    {
                        Id = clinicSpecialization.Id,
                        Name = localizedCs.Name
                    });
                }

                var localizedClinic = new LocalizedClinic();
                foreach (var localClinic in clinic.Localized)
                {
                    if (localClinic.Language.Code.ToLower() == lang.ToLower())
                        localizedClinic = localClinic;
                }

                //todo:доктора!!!

                dtoClinicList.Add(new DtoClinicSingleLang()
                {
                    LangCode = localizedClinic.Language.Code.ToLower(),
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
                    Specializations = dtoSpecializations
                });
            }

            return dtoClinicList;
        }

        public DtoClinicSingleLang GetClinicById(int id, string pathToImage, string lang)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }

            var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

            var dtoAddreses = new List<DtoAddressSingleLang>();
            foreach (var clinicAddress in clinic.Addresses)
            {
                var localizedAddress = new LocalizedAddress();
                foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses.ToList())
                {
                    if (string.Equals(clinicAddressLocalizedAddress.Language.Code.ToLower(), lang.ToLower(),
                        StringComparison.Ordinal))
                        localizedAddress = clinicAddressLocalizedAddress;
                }

                var localizedCity = new LocalizedCity();
                if (localizedAddress.City != null)
                    foreach (var cityLocalizedCity in localizedAddress.City.LocalizedCities)
                    {
                        if (string.Equals(cityLocalizedCity.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedCity = cityLocalizedCity;
                    }

                var phones = new List<DtoPhoneSingleLang>();
                foreach (var clinicPhone in clinicAddress.Phones)
                    foreach (var localizedPhone in clinicPhone.LocalizedPhones)
                        if (string.Equals(localizedPhone.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            phones.Add(new DtoPhoneSingleLang()
                            {
                                Id = localizedPhone.Id,
                                Desc = localizedPhone.Description,
                                Number = localizedPhone.Number
                            });

                dtoAddreses.Add(new DtoAddressSingleLang()
                {
                    Id = clinicAddress.Id,
                    Country = localizedAddress.Country,
                    City = localizedCity.Name,
                    Street = localizedAddress.Street,
                    Phones = phones
                });
            }

            var dtoSpecializations = new List<DtoClinicSpecializationSingleLangModels>();
            foreach (var clinicSpecialization in clinic.ClinicSpecializations)
            {
                var localizedCs = new LocalizedClinicSpecialization(); ;
                foreach (var localizedSpecialization in clinicSpecialization.LocalizedClinicSpecializations)
                {
                    if (localizedSpecialization.Language.Code.ToLower() == lang.ToLower())
                        localizedCs = localizedSpecialization;
                }

                dtoSpecializations.Add(new DtoClinicSpecializationSingleLangModels()
                {
                    Id = clinicSpecialization.Id,
                    Name = localizedCs.Name
                });
            }

            var localizedClinic = new LocalizedClinic();
            foreach (var localClinic in clinic.Localized)
            {
                if (localClinic.Language.Code.ToLower() == lang.ToLower())
                    localizedClinic = localClinic;
            }

            //todo:доктора!!!

            var dtoClinic = new DtoClinicSingleLang()
            {
                LangCode = localizedClinic.Language.Code.ToLower(),
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
                Specializations = dtoSpecializations
            };

            return dtoClinic;
        }

        public DtoPagedClinic GetPagedClinics(int perPage, int numPage, string pathToImage, string lang)
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
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

                var dtoAddreses = new List<DtoAddressSingleLang>();
                foreach (var clinicAddress in clinic.Addresses)
                {
                    var localizedAddress = new LocalizedAddress();
                    foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses.ToList())
                    {
                        if (string.Equals(clinicAddressLocalizedAddress.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedAddress = clinicAddressLocalizedAddress;
                    }

                    var localizedCity = new LocalizedCity();
                    foreach (var cityLocalizedCity in localizedAddress.City.LocalizedCities)
                    {
                        if (string.Equals(cityLocalizedCity.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedCity = cityLocalizedCity;
                    }

                    var phones = new List<DtoPhoneSingleLang>();
                    foreach (var clinicPhone in clinicAddress.Phones)
                        foreach (var localizedPhone in clinicPhone.LocalizedPhones)
                            if (string.Equals(localizedPhone.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                                phones.Add(new DtoPhoneSingleLang()
                                {
                                    Id = localizedPhone.Id,
                                    Desc = localizedPhone.Description,
                                    Number = localizedPhone.Number
                                });

                    dtoAddreses.Add(new DtoAddressSingleLang()
                    {
                        Id = clinicAddress.Id,
                        Country = localizedAddress.Country,
                        City = localizedCity.Name,
                        Street = localizedAddress.Street,
                        Phones = phones
                    });
                }

                var dtoSpecializations = new List<DtoClinicSpecializationSingleLangModels>();
                foreach (var clinicSpecialization in clinic.ClinicSpecializations)
                {
                    var localizedCs = new LocalizedClinicSpecialization(); ;
                    foreach (var localizedSpecialization in clinicSpecialization.LocalizedClinicSpecializations)
                    {
                        if (localizedSpecialization.Language.Code.ToLower() == lang.ToLower())
                            localizedCs = localizedSpecialization;
                    }

                    dtoSpecializations.Add(new DtoClinicSpecializationSingleLangModels()
                    {
                        Id = clinicSpecialization.Id,
                        Name = localizedCs.Name
                    });
                }

                var localizedClinic = new LocalizedClinic();
                foreach (var localClinic in clinic.Localized)
                {
                    if (localClinic.Language.Code.ToLower() == lang.ToLower())
                        localizedClinic = localClinic;
                }

                //todo:доктора!!!

                dtoClinicList.Add(new DtoClinicSingleLang()
                {
                    LangCode = localizedClinic.Language.Code.ToLower(),
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
                    Specializations = dtoSpecializations
                });
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
            string pathToImage, string lang)
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
            //IQueryable<Clinic> clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang);
            IQueryable<Clinic> clinics = null;
            switch (searchModel.CityId) //check whether CityId included in search request
            {
                case 0:
                    {
                        switch (searchModel.SpecializationIds.Any())
                        //check whether ClinicSpecialization included in search request
                        {
                            //case true:
                            case false:
                                {
                                    switch (searchModel.SearchWord == "") //check whether SearchWord included in search request
                                    {
                                        case true:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                                    Where(
                                                        x =>
                                                            !searchModel.SpecializationIds.Except(
                                                                x.ClinicSpecializations.Select(y => y.Id)).Any());
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                                    Where(x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any() ||
                                                                x.ClinicSpecializations.Any(
                                                                    z => z.LocalizedClinicSpecializations.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                                                                 !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any())
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
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang);
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                                    Where(x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                                            x.ClinicSpecializations.Any(z => z.LocalizedClinicSpecializations.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower())));
                                                break;
                                            }
                                    }

                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        switch (searchModel.SpecializationIds.Any())
                        //check whether ClinicSpecialization included in search request
                        {
                            case true:
                                {
                                    switch (searchModel.SearchWord == "")
                                    {
                                        case true:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                                    Where(x => x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId) &&
                                                               !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any());
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang).
                                                    Where(x => (x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId) &&
                                                               !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any()) ||
                                                               (x.ClinicSpecializations.Any(z => z.LocalizedClinicSpecializations.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId) &&
                                                               !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any())));
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
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending,lang).
                                                    Where(x => x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId));
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending,lang).
                                                    Where(x => (x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId)) ||
                                                               (x.ClinicSpecializations.Any(z => z.LocalizedClinicSpecializations.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower())) &&
                                                               x.Addresses.Any(y => y.LocalizedAddresses.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId)));
                                                break;
                                            }
                                    }
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
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

                var dtoAddreses = new List<DtoAddressSingleLang>();
                foreach (var clinicAddress in clinic.Addresses)
                {
                    var localizedAddress = new LocalizedAddress();
                    foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses.ToList())
                    {
                        if (string.Equals(clinicAddressLocalizedAddress.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                            localizedAddress = clinicAddressLocalizedAddress;
                    }

                    var localizedCity = new LocalizedCity();
                    if (localizedAddress.City != null)
                        foreach (var cityLocalizedCity in localizedAddress.City.LocalizedCities)
                        {
                            if (string.Equals(cityLocalizedCity.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                                localizedCity = cityLocalizedCity;
                        }

                    var phones = new List<DtoPhoneSingleLang>();
                    foreach (var clinicPhone in clinicAddress.Phones)
                        foreach (var localizedPhone in clinicPhone.LocalizedPhones)
                            if (string.Equals(localizedPhone.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                                phones.Add(new DtoPhoneSingleLang()
                                {
                                    Id = localizedPhone.Id,
                                    Desc = localizedPhone.Description,
                                    Number = localizedPhone.Number
                                });

                    dtoAddreses.Add(new DtoAddressSingleLang()
                    {
                        Id = clinicAddress.Id,
                        Country = localizedAddress.Country,
                        City = localizedCity.Name,
                        Street = localizedAddress.Street,
                        Phones = phones
                    });
                }

                var dtoSpecializations = new List<DtoClinicSpecializationSingleLangModels>();
                foreach (var clinicSpecialization in clinic.ClinicSpecializations)
                {
                    var localizedCs = new LocalizedClinicSpecialization(); ;
                    foreach (var localizedSpecialization in clinicSpecialization.LocalizedClinicSpecializations)
                    {
                        if (localizedSpecialization.Language.Code.ToLower() == lang.ToLower())
                            localizedCs = localizedSpecialization;
                    }

                    dtoSpecializations.Add(new DtoClinicSpecializationSingleLangModels()
                    {
                        Id = clinicSpecialization.Id,
                        Name = localizedCs.Name
                    });
                }

                var localizedClinic = new LocalizedClinic();
                foreach (var localClinic in clinic.Localized)
                {
                    if (localClinic.Language.Code.ToLower() == lang.ToLower())
                        localizedClinic = localClinic;
                }

                //todo:доктора!!!

                dtoClinicList.Add(new DtoClinicSingleLang()
                {
                    LangCode = localizedClinic.Language.Code.ToLower(),
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
                    Specializations = dtoSpecializations
                });
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
            _searchService.RefreshCache();
        }

        public void Update(int id, string name)
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
    }
}