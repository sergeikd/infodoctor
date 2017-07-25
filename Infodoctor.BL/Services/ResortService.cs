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

        public ResortService(IResortRepository resort, ISearchService search)
        {
            if (resort == null) throw new ArgumentNullException(nameof(resort));
            if (search == null) throw new ArgumentNullException(nameof(search));
            _resort = resort;
            _search = search;
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

            var dtoResortLocals = resort.Localized.Select(local => new DtoResortMultiLangLocalized()
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

        public DtoPagedResorts SearchResorts(int perPage, int numPage, DtoResortSearchModel searchModel, string pathToImage, string lang)
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

            IQueryable<Resort> resorts;

            switch (searchModel.CityId)
            {
                case 0:
                    {
                        switch (searchModel.SearchWord == "")
                        {
                            case true:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang);
                                    break;
                                }
                            default:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang)
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
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang)
                                        .Where(r => r.Address.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).City.Id == searchModel.CityId);
                                    break;
                                }
                            default:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending, lang)
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

        public void Add(DtoResortMultiLang res)
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
    }
}
