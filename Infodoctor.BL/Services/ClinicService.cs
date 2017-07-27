using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infodoctor.BL.Services
{
    public class ClinicService : IClinicService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly IClinicTypeRepository _clinicTypeRepository;
        private readonly IClinicAddressesRepository _clinicAddressesRepository;
        private readonly IClinicPhonesRepository _clinicPhonesRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly ISearchService _searchService;

        public ClinicService(IClinicRepository clinicRepository,
            ISearchService searchService,
            ILanguageRepository languageRepository,
            IClinicTypeRepository clinicTypeRepository,
            IClinicAddressesRepository clinicAddressesRepository,
            IClinicPhonesRepository clinicPhonesRepository,
            ICountryRepository countryRepository,
            ICitiesRepository citiesRepository)
        {
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _citiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
            _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
            _clinicTypeRepository = clinicTypeRepository ?? throw new ArgumentNullException(nameof(clinicTypeRepository));
            _clinicAddressesRepository = clinicAddressesRepository ?? throw new ArgumentNullException(nameof(clinicAddressesRepository));
            _clinicPhonesRepository = clinicPhonesRepository ?? throw new ArgumentNullException(nameof(clinicPhonesRepository));
            _clinicRepository = clinicRepository ?? throw new ArgumentNullException(nameof(clinicRepository));
        }

        public IEnumerable<DtoClinicSingleLang> GetAllClinics(string pathToClinicImage, string lang)
        {
            var clinicList = _clinicRepository.GetСlinics().ToList();
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
            var clinic = _clinicRepository.GetClinic(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }

            var dtoClinic = ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic);

            return dtoClinic;
        }

        public DtoClinicMultiLang GetClinicById(int id, string pathToClinicImage)
        {
            var clinic = _clinicRepository.GetClinic(id);
            if (clinic == null)
                throw new ApplicationException("Clinic not found");

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
                        Country = address.Country.LocalizedCountries
                            ?.First(l => l.Language.Code.ToLower() == local.Language.Code.ToLower())
                            ?.Name,
                        City = address.City.LocalizedCities
                            ?.First(l => l.Language.Code.ToLower() == local.Language.Code.ToLower())
                            ?.Name,
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
                var type = string.Empty;

                foreach (var localizedClinicType in clinic.Type.Localized)
                    if (localizedClinicType.Language == local.Language)
                        type = localizedClinicType.Name;

                var localClinic = new LocalizedDtoClinic()
                {
                    Id = local.Id,
                    Name = local.Name,
                    Specializations = local.Specializations.Split(',', ';', '|').ToList(),
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
                Childish = clinic.Childish,
                Favorite = clinic.Favorite,
                FavouriteExpireDate = clinic.FavouriteExpireDate,
                Recommended = clinic.Recommended,
                RecommendedExpireDate = clinic.RecommendedExpireDate,
                ClinicAddress = dtoAddreses,
                DoctorsIdList = doctorsId,
                LocalizedClinic = localClinics
            };

            return dtoClinic;
        }

        public DtoPagedClinic GetPagedClinics(int perPage, int numPage, string pathToClinicImage, string lang, int type)
        {
            if (perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var clinics = _clinicRepository.GetСlinics(type);

            var pagedList = new PagedList<Clinic>(clinics, perPage, numPage);

            if (!pagedList.Any())
                return null;

            var dtoClinicList = pagedList
                    .Select(clinic => ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic))
                    .ToList();

            var pagedDtoClinicList = new DtoPagedClinic
            {
                Clinics = dtoClinicList,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };
            return pagedDtoClinicList;
        }

        public DtoPagedClinic SearchClinics(int perPage, int numPage, DtoClinicSearchModel searchModel, string pathToClinicImage, string lang, int type)
        {
            if (perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

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
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang, type);
                                    break;
                                }
                            default:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang, type)
                                        .Where(
                                            x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower())
                                                     .Name.ToLower()
                                                     .Contains(searchModel.SearchWord.ToLower()) ||
                                                 x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower())
                                                     .Specializations.ToLower()
                                                     .Contains(searchModel.SearchWord.ToLower())
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
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang, type)
                                        .Where(
                                            x => x.Addresses.Any(y => y.City.Id == searchModel.CityId)
                                        );
                                    break;
                                }
                            default:
                                {
                                    clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending, lang, type)
                                        .Where(
                                            x => x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower())
                                                     .Name.ToLower()
                                                     .Contains(searchModel.SearchWord.ToLower()) ||
                                                 x.Localized.FirstOrDefault(ls => ls.Language.Code.ToLower() == lang.ToLower())
                                                     .Specializations.ToLower()
                                                     .Contains(searchModel.SearchWord.ToLower()) &&
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
                return null;

            var dtoClinicList = pagedList.Select(clinic => ConvertEntityToDtoSingleLang(lang, pathToClinicImage, clinic)).ToList();

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
            if (clinic == null)
                throw new ArgumentNullException(nameof(clinic));

            var entityClinic = BuildClinicEntityFromDto(clinic);

            var entityAddress = clinic.ClinicAddress
                .Select(dtoAddressMultiLang => BuildAddressEntityFromDto(dtoAddressMultiLang, entityClinic))
                .ToList();
            var entityLocalizedAddreses = new List<LocalizedAddress>();
            foreach (var address in entityAddress)
                entityLocalizedAddreses.AddRange(address.LocalizedAddresses);

            var entityPhones = new List<Phone>();
            foreach (var address in entityAddress)
                entityPhones.AddRange(address.Phones);

            var entityLocalizedPhones = new List<LocalizedPhone>();
            foreach (var phone in entityPhones)
                entityLocalizedPhones.AddRange(phone.LocalizedPhones);

            //добавление клиники
            _clinicRepository.Add(entityClinic);

            //добавление телефонов
            foreach (var phone in entityPhones)
                _clinicPhonesRepository.Add(phone);

            //добавление адрессов
            foreach (var address in entityAddress)
                _clinicAddressesRepository.Add(address);

           _searchService.RefreshCache();
        }

        public void AddMany(IEnumerable<DtoClinicMultiLang> clinics)
        {
            foreach (var clinic in clinics)
            {
                var entityClinic = BuildClinicEntityFromDto(clinic);

                var entityAddress = clinic.ClinicAddress
                    .Select(dtoAddressMultiLang => BuildAddressEntityFromDto(dtoAddressMultiLang, entityClinic))
                    .ToList();
                var entityLocalizedAddreses = new List<LocalizedAddress>();
                foreach (var address in entityAddress)
                    entityLocalizedAddreses.AddRange(address.LocalizedAddresses);

                var entityPhones = new List<Phone>();
                foreach (var address in entityAddress)
                    entityPhones.AddRange(address.Phones);

                var entityLocalizedPhones = new List<LocalizedPhone>();
                foreach (var phone in entityPhones)
                    entityLocalizedPhones.AddRange(phone.LocalizedPhones);

                //добавление клиники
                _clinicRepository.Add(entityClinic);

                //добавление телефонов
                foreach (var phone in entityPhones)
                    _clinicPhonesRepository.Add(phone);

                //добавление адрессов
                foreach (var address in entityAddress)
                    _clinicAddressesRepository.Add(address);        
            }

            _searchService.RefreshCache();
        }

        public void Update(DtoClinicMultiLang clinic)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name));

            //var c = _clinicRepository.GetClinic(id);
            //if (c != null)
            //{
            //    c.Name = name;
            //    _clinicRepository.Update(c);
            //    _searchService.RefreshCache();
            //}
        }

        public void Delete(int id)
        {
            var cp = _clinicRepository.GetClinic(id);

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
                              where string.Equals(localizedPhone.Language.Code.ToLower(), lang.ToLower(),
                                  StringComparison.Ordinal)
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

            var type = 0;
            if (clinic.Type != null)
                foreach (var localizedClinicType in clinic.Type.Localized)
                    if (localizedClinicType.Language.Code.ToLower() == lang)
                        type = localizedClinicType.Id;

            var doctors = clinic.Doctors.Select(doctor => doctor.Id).ToList();

            var dtoClinic = new DtoClinicSingleLang()
            {
                LangCode = localizedClinic.Language?.Code.ToLower(),
                Id = clinic.Id,
                Name = localizedClinic.Name,
                Type = type,
                Email = clinic.Email,
                Site = clinic.Site,
                RatePoliteness = clinic.RatePoliteness,
                RateQuality = clinic.RateQuality,
                RatePrice = clinic.RatePrice,
                RateAverage = clinic.RateAverage,
                ReviewCount = clinic.ClinicReviews.Count,
                Childish = clinic.Childish,
                Favorite = clinic.Favorite,
                FavouriteExpireDate = clinic.FavouriteExpireDate,
                Recommended = clinic.Recommended,
                RecommendedExpireDate = clinic.RecommendedExpireDate,
                Images = imagesList,
                Addresses = dtoAddreses,
                Specializations = localizedClinic.Specializations.Split(',', ';', '|').ToList(),
                DoctorsIdList = doctors
            };

            return dtoClinic;
        }

        private Clinic BuildClinicEntityFromDto(DtoClinicMultiLang clinic)
        {
            if (clinic == null)
                throw new ArgumentNullException(nameof(clinic));

            var locals = new List<LocalizedClinic>();
            foreach (var localizedDtoClinic in clinic.LocalizedClinic)
            {
                Language lang = null;

                try
                {
                    lang = _languageRepository.GetLanguageByCode(localizedDtoClinic.LangCode.ToLower());
                }
                catch (Exception)
                {

                    throw new ApplicationException($"Lang {localizedDtoClinic.LangCode} not found");

                }

                locals.Add(new LocalizedClinic()
                {
                    Language = lang,
                    Name = localizedDtoClinic.Name,
                    Specializations = localizedDtoClinic.LangCode
                });

            }


            ClinicType type = null;
            try
            {
                type = _clinicTypeRepository.GeType(clinic.Type);
            }
            catch (Exception)
            {
                //throw new ApplicationException($"Type {clinic.Type} not found");
            }

            var images = clinic.Images.Select(image =>
                    new ImageFile() { Name = image }
                ).ToList();

            var entityClinic = new Clinic()
            {
                Id = clinic.Id,
                Email = clinic.Email,
                Site = clinic.Site,
                Childish = clinic.Childish,
                Favorite = clinic.Favorite,
                FavouriteExpireDate = clinic.FavouriteExpireDate,
                Recommended = clinic.Recommended,
                RecommendedExpireDate = clinic.RecommendedExpireDate,
                Type = type,
                Localized = locals,
                ImageName = images
            };

            // var addreses = clinic.ClinicAddress.Select(dtoAddressMultiLang => BuildAddressEntityFromDto(dtoAddressMultiLang, entityClinic)).ToList();

            //  entityClinic.Addresses = addreses;

            return entityClinic;
        }

        private Address BuildAddressEntityFromDto(DtoAddressMultiLang address, Clinic clinic)
        {
            Country country = null;
            City city = null;

            try
            {
                country = _countryRepository.GetCountryById(address.CountryId);
            }
            catch (Exception)
            {
                throw new ApplicationException($"Country {address.CountryId} not found");
            }

            try
            {
                city = _citiesRepository.GetCityById(address.CityId);
            }
            catch (Exception)
            {
                throw new ApplicationException($"City {address.CityId} not found");
            }


            var locals = new List<LocalizedAddress>();
            foreach (var localizedDtoAddress in address.LocalizedAddress)
            {
                Language lang;

                try
                {
                    lang = _languageRepository.GetLanguageByCode(localizedDtoAddress.LangCode.ToLower());
                }
                catch (Exception)
                {
                    throw new ApplicationException($"Lang {localizedDtoAddress.LangCode} not found");

                }

                locals.Add(new LocalizedAddress()
                {
                    Street = localizedDtoAddress.Street,
                    Language = lang
                });
            }

            var entityAddress = new Address()
            {
                Clinic = clinic,
                Country = country,
                City = city,
                Lat = address.Coords?.Lat,
                Lng = address.Coords?.Lng,
                LocalizedAddresses = locals
            };

            var phones = address.Phones.Select(dtoPhoneMultiLang => BuildPhoneEntityFromDto(dtoPhoneMultiLang)).ToList();

            entityAddress.Phones = phones;

            return entityAddress;
        }

        private Phone BuildPhoneEntityFromDto(DtoPhoneMultiLang phone)
        {
            var locals = new List<LocalizedPhone>();
            foreach (var localizedDtoPhone in phone.LocalizedPhone)
            {
                Language lang = null;

                try
                {
                    lang = _languageRepository.GetLanguageByCode(localizedDtoPhone.LangCode.ToLower());
                }
                catch (Exception)
                {
                    throw new ApplicationException($"Lang {localizedDtoPhone.LangCode} not found");
                }

                locals.Add(new LocalizedPhone()
                {
                    Description = localizedDtoPhone.Desc,
                    Number = localizedDtoPhone.Number,
                    Language = lang
                });
            }

            var entityPhone = new Phone()
            {
                LocalizedPhones = locals
            };

            return entityPhone;
        }
    }
}