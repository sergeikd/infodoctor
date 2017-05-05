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

        public IEnumerable<DtoResort> GetAllResorts(string pathToImage)
        {
            var resorts = _resort.GetAllResorts().ToList();

            var dtoResortList = new List<DtoResort>();

            foreach (var resort in resorts)
            {
                var dtoResortAddress = new DtoAddress()
                {
                    Country = resort.Address.Country,
                    City = resort.Address.City.Name,
                    Street = resort.Address.Street,
                    ClinicPhones = new List<DtoPhone>()
                };

                foreach (var phone in resort.Address.Phones)
                {
                    var dtoClinicPhone = new DtoPhone()
                    {
                        Desc = phone.Description,
                        Phone = phone.Number
                    };
                    dtoResortAddress.ClinicPhones.Add(dtoClinicPhone);
                }

                var dtoResort = new DtoResort()
                {
                    Id = resort.Id,
                    Name = resort.Name,
                    Email = resort.Email,
                    Site = resort.Site,
                    Specialisations = resort.Specialisations,
                    Address = dtoResortAddress,
                    RateAverage = resort.RateAverage,
                    RatePoliteness = resort.RatePoliteness,
                    RatePrice = resort.RatePrice,
                    RateQuality = resort.RateQuality,
                    ReviewCount = resort.Reviews.Count,
                    Favorite = resort.Favorite,
                    Image = pathToImage + resort.ImageName
                };

                dtoResortList.Add(dtoResort);
            }

            return dtoResortList;
        }

        public DtoPagedResorts GetPagedResorts(int perPage, int numPage, string pathToImage)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }

            var resorts = _resort.GetAllResorts();
            var pagedList = new PagedList<Resort>(resorts, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }

            var dtoResortList = new List<DtoResort>();

            foreach (var resort in pagedList)
            {
                var dtoResortAddress = new DtoAddress()
                {
                    Country = resort.Address.Country,
                    City = resort.Address.City.Name,
                    Street = resort.Address.Street,
                    ClinicPhones = new List<DtoPhone>()
                };

                foreach (var phone in resort.Address.Phones)
                {
                    var dtoClinicPhone = new DtoPhone()
                    {
                        Desc = phone.Description,
                        Phone = phone.Number
                    };
                    dtoResortAddress.ClinicPhones.Add(dtoClinicPhone);
                }

                var dtoResort = new DtoResort()
                {
                    Id = resort.Id,
                    Name = resort.Name,
                    Email = resort.Email,
                    Site = resort.Site,
                    Specialisations = resort.Specialisations,
                    Address = dtoResortAddress,
                    RateAverage = resort.RateAverage,
                    RatePoliteness = resort.RatePoliteness,
                    RatePrice = resort.RatePrice,
                    RateQuality = resort.RateQuality,
                    ReviewCount = resort.Reviews.Count,
                    Favorite = resort.Favorite,
                    Image = pathToImage + resort.ImageName
                };

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

        public DtoPagedResorts SearchResorts(int perPage, int numPage, DtoResortSearchModel searchModel, string pathToImage)
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
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending);
                                    break;
                                }
                            default:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending)
                                        .Where(r => r.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                                    r.Specialisations.ToLower().Contains(searchModel.SearchWord.ToLower()));
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
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending)
                                        .Where(r => r.Address.City.Id == searchModel.CityId);
                                    break;
                                }
                            default:
                                {
                                    resorts = _resort.GetSortedResorts(searchModel.SortBy, descending)
                                        .Where(r => (r.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                     r.Address.City.Id == searchModel.CityId) || (r.Specialisations.ToLower().Contains(searchModel.SearchWord.ToLower()) && r.Address.City.Id == searchModel.CityId));
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

            var dtoResortList = new List<DtoResort>();

            foreach (var resort in pagedList)
            {
                var dtoResortAddress = new DtoAddress()
                {
                    Country = resort.Address.Country,
                    City = resort.Address.City.Name,
                    Street = resort.Address.Street,
                    ClinicPhones = new List<DtoPhone>()
                };

                foreach (var phone in resort.Address.Phones)
                {
                    var dtoClinicPhone = new DtoPhone()
                    {
                        Desc = phone.Description,
                        Phone = phone.Number
                    };
                    dtoResortAddress.ClinicPhones.Add(dtoClinicPhone);
                }

                var dtoResort = new DtoResort()
                {
                    Id = resort.Id,
                    Name = resort.Name,
                    Email = resort.Email,
                    Site = resort.Site,
                    Specialisations = resort.Specialisations,
                    Address = dtoResortAddress,
                    RateAverage = resort.RateAverage,
                    RatePoliteness = resort.RatePoliteness,
                    RatePrice = resort.RatePrice,
                    RateQuality = resort.RateQuality,
                    ReviewCount = resort.Reviews.Count,
                    Favorite = resort.Favorite,
                    Image = pathToImage + resort.ImageName
                };

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

        public DtoResort GetResortById(int id, string pathToImage)
        {
            var resort = _resort.GetResortById(id);
            if (resort == null)
                throw new ApplicationException("Resort not found");

            var dtoResortAddress = new DtoAddress()
            {
                Country = resort.Address.Country,
                City = resort.Address.City.Name,
                Street = resort.Address.Street,
                ClinicPhones = new List<DtoPhone>()
            };

            foreach (var phone in resort.Address.Phones)
            {
                var dtoClinicPhone = new DtoPhone()
                {
                    Desc = phone.Description,
                    Phone = phone.Number
                };
                dtoResortAddress.ClinicPhones.Add(dtoClinicPhone);
            }

            var dtoResort = new DtoResort()
            {
                Id = resort.Id,
                Name = resort.Name,
                Email = resort.Email,
                Site = resort.Site,
                Specialisations = resort.Specialisations,
                Address = dtoResortAddress,
                RateAverage = resort.RateAverage,
                RatePoliteness = resort.RatePoliteness,
                RatePrice = resort.RatePrice,
                RateQuality = resort.RateQuality,
                ReviewCount = resort.Reviews.Count,
                Favorite = resort.Favorite,
                Image = pathToImage + resort.ImageName
            };


            return dtoResort;
        }

        public void Add(DtoResort res)
        {
            throw new NotImplementedException();
        }

        public void Update(DtoResort res)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var res = _resort.GetResortById(id);
            if (res == null)
                throw new ApplicationException("Resort not found");

            _resort.Delete(res);
            _search.RefreshCache();
        }
    }
}
