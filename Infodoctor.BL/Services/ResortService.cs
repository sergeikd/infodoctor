using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ResortService : IResortService
    {
        private readonly IResortRepository _resort;
        private readonly ISearchService _search;
        private readonly ILanguageRepository _languageRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly IResortTypeRepository _resortTypeRepository;

        public ResortService(IResortRepository resort,
            ISearchService search,
            ILanguageRepository languageRepository,
            ICountryRepository countryRepository,
            ICitiesRepository citiesRepository,
            IResortTypeRepository resortTypeRepository)
        {
            if (resort == null) throw new ArgumentNullException(nameof(resort));
            if (search == null) throw new ArgumentNullException(nameof(search));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));
            if (countryRepository == null) throw new ArgumentNullException(nameof(countryRepository));
            if (citiesRepository == null) throw new ArgumentNullException(nameof(citiesRepository));
            if (resortTypeRepository == null) throw new ArgumentNullException(nameof(resortTypeRepository));
            _resort = resort;
            _search = search;
            _languageRepository = languageRepository;
            _countryRepository = countryRepository;
            _citiesRepository = citiesRepository;
            _resortTypeRepository = resortTypeRepository;
        }

        public IEnumerable<DtoResortSingleLang> GetAllResorts(string pathToImage, string lang)
        {
            var resorts = _resort.GetAllResorts(0).ToList();
            return resorts.Select(resort => ConvertEntityToDtoSingleLang(lang, pathToImage, resort)).ToList();
        }

        public DtoPagedResorts GetPagedResorts(int perPage, int numPage, string pathToImage, string lang, int type)
        {
            if (perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var resorts = _resort.GetAllResorts(type);
            var pagedList = new PagedList<Resort>(resorts, perPage, numPage);

            if (!pagedList.Any())
                return null;

            var dtoResortList = pagedList.Select(resort => ConvertEntityToDtoSingleLang(lang, pathToImage, resort)).ToList();

            var pagedDtoResorts = new DtoPagedResorts()
            {
                Resorts = dtoResortList,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };

            return pagedDtoResorts;
        }

        public DtoResortSingleLang GetResortById(int id, string pathToImage, string lang)
        {
            var resort = _resort.GetResortById(id);
            if (resort == null)
                throw new ApplicationException("Resort not found");

            var dtoResort = ConvertEntityToDtoSingleLang(lang, pathToImage, resort);

            return dtoResort;
        }

        public DtoResortMultiLang GetResortById(int id, string pathToImage)
        {
            var resort = _resort.GetResortById(id);
            if (resort == null)
                throw new ApplicationException("Resort not found");

            var address = resort.Address;
            var dtoAddress = new DtoAddressMultiLang()
            {
                Id = address.Id,
                Coords = new Coords() { Lat = address.Lat, Lng = address.Lng },
                Phones = new List<DtoPhoneMultiLang>(),
                LocalizedAddress = new List<LocalizedDtoAddress>()
            };

            foreach (var local in address.Localized)
            {
                var localAddress = new LocalizedDtoAddress()
                {
                    Id = local.Id,
                    Country = local.Country,
                    City = local.City.LocalizedCities?.First(l => l.Language.Code.ToLower() == local.Language.Code.ToLower()).Name,
                    Street = local.Street,
                    LangCode = local.Language?.Code.ToLower()
                };
                dtoAddress.LocalizedAddress.Add(localAddress);
            }

            foreach (var phone in address.Phones)
            {
                var localPhones = new List<LocalizedDtoPhone>();
                foreach (var local in phone.Localized)
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

                var dtoPhone = new DtoPhoneMultiLang
                {
                    Id = phone.Id,
                    LocalizedPhone = localPhones
                };

                dtoAddress.Phones.Add(dtoPhone);
            }

            var dtoResortLocals = resort.Localized.Select(local => new LocalizedDtoResort()
            {
                Id = local.Id,
                Name = local.Name,
                Manipulations = local.Manipulations.Split(',', ';', '|').ToList()
            }).ToList();

            var dtoResort = new DtoResortMultiLang()
            {
                Id = resort.Id,
                Email = resort.Email,
                Site = resort.Site,
                Type = resort.Type.Id,
                RateAverage = resort.RateAverage,
                RatePoliteness = resort.RatePoliteness,
                RatePrice = resort.RatePrice,
                RateQuality = resort.RateQuality,
                ReviewCount = resort.Reviews.Count,
                Image = pathToImage + resort.ImageName,
                Address = dtoAddress,
                LocalizedResort = dtoResortLocals,
                Favorite = resort.Favorite,
                FavouriteExpireDate = resort.FavouriteExpireDate,
                Recommended = resort.Recommended,
                RecommendedExpireDate = resort.RecommendedExpireDate
            };
            return dtoResort;
        }

        public DtoPagedResorts SearchResorts(int perPage, int numPage, DtoResortSearchModel searchModel, string pathToImage, string lang, int type)
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

            IQueryable<Resort> resorts;

            switch (searchModel.CityId)
            {
                case 0:
                    {
                        switch (searchModel.SearchWord == "")
                        {
                            case true:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang, type);
                                    break;
                                }
                            default:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang, type)
                                        .Where(r =>
                                                r.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                                r.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Manipulations.ToLower().Contains(searchModel.SearchWord.ToLower())
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
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang, type)
                                        .Where(r => r.Address.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId);
                                    break;
                                }
                            default:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang, type)
                                        .Where(r =>
                                            r.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                            r.Address.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId ||
                                            r.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Manipulations.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                            r.Address.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId);
                                    break;
                                }
                        }

                        break;
                    }
            }

            var pagedList = new PagedList<Resort>(resorts, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }

            var dtoResortList = new List<DtoResortSingleLang>();

            foreach (var resort in pagedList)
            {
                var dtoResort = ConvertEntityToDtoSingleLang(lang, pathToImage, resort);
                dtoResortList.Add(dtoResort);
            }

            var pagedDtoResorts = new DtoPagedResorts()
            {
                Resorts = dtoResortList,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };

            return pagedDtoResorts;
        }

        public void Add(DtoResortMultiLang resort)
        {
            if (resort == null)
                throw new ArgumentNullException(nameof(resort));

            var entityResort = BuildResortEntityFromDto(resort);
            var entityAddress = BuildAddressEntityFromDto(resort.Address, entityResort);
            var entityLocalizedAddreses = entityAddress.Localized;
            var entityPhones = entityAddress.Phones;
            var entityLocalizedPhones = new List<LocalizedResortPhone>();
            foreach (var phone in entityPhones)
                entityLocalizedPhones.AddRange(phone.Localized);
            /*
            //добавление клиники
            _clinicRepository.Add(entityClinic);

            //добавление телефонов
            foreach (var phone in entityPhones)
                _clinicPhonesRepository.Add(phone);

            //добавление адрессов
            foreach (var address in entityAddress)
                _clinicAddressesRepository.Add(address);

            _searchService.RefreshCache();
            */
        }

        public void AddMany(List<DtoResortMultiLang> resList)
        {
            throw new NotImplementedException();
        }

        public void Update(DtoResortMultiLang res)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, string pathToImage)
        {
            var res = _resort.GetResortById(id);
            if (res == null)
                throw new ApplicationException("Resort not found");

            var imgPath = pathToImage + res.ImageName;
            if (File.Exists(imgPath))
                File.Delete(imgPath);

            _resort.Delete(res);
            _search.RefreshCache();
        }

        private static DtoResortSingleLang ConvertEntityToDtoSingleLang(string lang, string pathToImage, Resort resort)
        {
            LocalizedResort localizedResort = null;
            LocalizedCity localizedCity = null;
            LocalizedResortAddress localizedAddress = null;

            try
            {
                localizedResort = resort.Localized.First(l => l.Language.Code.ToLower() == lang.ToLower());
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                localizedAddress = resort.Address.Localized.First(l => l.Language.Code.ToLower() == lang.ToLower());
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                localizedCity = localizedAddress?.City.LocalizedCities.First(l => l.Language.Code.ToLower() == lang.ToLower());
            }
            catch (Exception)
            {
                // ignored
            }

            var dtoResortAddress = new DtoAddressSingleLang()
            {
                Id = resort.Address.Id,
                Country = localizedAddress?.Country,
                City = localizedCity?.Name,
                Street = localizedAddress?.Street,
                Phones = new List<DtoPhoneSingleLang>(),
                Coords = new Coords() { Lat = resort.Address?.Lat, Lng = resort.Address?.Lng }
            };

            foreach (var phone in resort.Address?.Phones)
            {
                var locals = phone.Localized.Where(l => l.Language.Code.ToLower() == lang.ToLower()).ToList();

                if (!locals.Any()) continue;

                foreach (var local in locals)
                {
                    var dtoClinicPhone = new DtoPhoneSingleLang()
                    {
                        Desc = local.Description,
                        Number = local.Number
                    };
                    dtoResortAddress.Phones.Add(dtoClinicPhone);
                }
            }

            foreach (var resortType in resort.Type.Localized)
                if (resortType.Language.Code.ToLower() == lang)
                {
                }

            var dtoResort = new DtoResortSingleLang()
            {
                Id = resort.Id,
                LangCode = localizedResort?.Language.Code.ToLower(),
                Name = localizedResort?.Name,
                Email = resort.Email,
                Type = resort.Type.Id,
                Site = resort.Site,
                Manipulations = localizedResort?.Manipulations.Split(',', ';', '|').ToList(),
                Address = dtoResortAddress,
                RateAverage = resort.RateAverage,
                RatePoliteness = resort.RatePoliteness,
                RatePrice = resort.RatePrice,
                RateQuality = resort.RateQuality,
                ReviewCount = resort.Reviews.Count,
                Favorite = resort.Favorite,
                FavouriteExpireDate = resort.FavouriteExpireDate,
                Recommended = resort.Recommended,
                RecommendedExpireDate = resort.RecommendedExpireDate,
                Image = pathToImage + resort.ImageName
            };

            return dtoResort;
        }

        private Resort BuildResortEntityFromDto(DtoResortMultiLang resort)
        {
            if (resort == null)
                throw new ArgumentNullException(nameof(resort));

            var locals = new List<LocalizedResort>();
            foreach (var localizedDtoClinic in resort.LocalizedResort)
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

                var manipulations = localizedDtoClinic.Manipulations.Aggregate(string.Empty, (current, s) => current + (s + '|'));
                manipulations = manipulations.Remove(manipulations.Length - 1, 1);

                locals.Add(new LocalizedResort()
                {
                    Language = lang,
                    Name = localizedDtoClinic.Name,
                    Manipulations = manipulations
                });
            }

            ResortType type = null;
            try
            {
                type = _resortTypeRepository.GeType(resort.Type);
            }
            catch (Exception)
            {
                //throw new ApplicationException($"Type {resort.Type} not found");
            }

            //var images = resort.Images.Select(image =>
            //    new ImageFile() { Name = image }
            //).ToList();
            //todo раобраться с изображениями

            var entityClinic = new Resort()
            {
                Id = resort.Id,
                Email = resort.Email,
                Site = resort.Site,
                Favorite = resort.Favorite,
                FavouriteExpireDate = resort.FavouriteExpireDate,
                Recommended = resort.Recommended,
                RecommendedExpireDate = resort.RecommendedExpireDate,
                Type = type,
                Localized = locals,
                //ImageName = images
            };

            // var addreses = resort.ClinicAddress.Select(dtoAddressMultiLang => BuildAddressEntityFromDto(dtoAddressMultiLang, entityClinic)).ToList();
            //  entityClinic.Addresses = addreses;

            return entityClinic;
        }

        private ResortAddress BuildAddressEntityFromDto(DtoAddressMultiLang address, Resort clinic)
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

            var locals = new List<LocalizedResortAddress>();
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

                locals.Add(new LocalizedResortAddress()
                {
                    Street = localizedDtoAddress.Street,
                    Language = lang
                });
            }

            var entityAddress = new ResortAddress()
            {
                Resort = clinic,
                Country = country,
                City = city,
                Lat = address.Coords?.Lat,
                Lng = address.Coords?.Lng,
                Localized = locals
            };

            var phones = address.Phones.Select(dtoPhoneMultiLang => BuildPhoneEntityFromDto(dtoPhoneMultiLang)).ToList();

            entityAddress.Phones = phones;

            return entityAddress;
        }

        private ResortPhone BuildPhoneEntityFromDto(DtoPhoneMultiLang phone)
        {
            var locals = new List<LocalizedResortPhone>();
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

                locals.Add(new LocalizedResortPhone()
                {
                    Description = localizedDtoPhone.Desc,
                    Number = localizedDtoPhone.Number,
                    Language = lang
                });
            }

            var entityPhone = new ResortPhone()
            {
                Localized = locals
            };

            return entityPhone;
        }
    }
}
