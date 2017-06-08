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

        public IEnumerable<DtoClinic> GetAllClinics(string pathToImage, string lang)
        {
            var clinicList = _clinicRepository.GetAllСlinics().ToList();

            var dtoClinicList = new List<DtoClinic>();

            foreach (var clinic in clinicList)
            {
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicPhoneList = new List<DtoPhone>();
                    foreach (var clinicAddressPhone in clinicAddress.Phones)
                    {
                        var localizedDtoClinicPhoneList = new List<LocalizedDtoPhone>();
                        foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                        {
                            if (localizedClinicPhone.Language.Code.ToLower() == lang.ToLower())
                            {
                                localizedDtoClinicPhoneList.Add(new LocalizedDtoPhone()
                                {
                                    Id = localizedClinicPhone.Id,
                                    Desc = localizedClinicPhone.Description,
                                    Phone = localizedClinicPhone.Number,
                                    LangCode = localizedClinicPhone.Language.Code
                                });

                            }

                        }
                        dtoClinicPhoneList.Add(new DtoPhone()
                        {
                            Id = clinicAddressPhone.Id,
                            LocalizedDtoPhones = localizedDtoClinicPhoneList
                        });
                    }

                    var localizedDtoClinicAddressList = new List<LocalizedDtoAddress>();
                    foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses)
                    {
                        if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoClinicAddressList.Add(new LocalizedDtoAddress()
                            {
                                Country = clinicAddressLocalizedAddress.Country,
                                City = clinicAddressLocalizedAddress.City.LocalisedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                                Street = clinicAddressLocalizedAddress.Street,
                                LangCode = clinicAddressLocalizedAddress.Language.Code
                            });
                        }
                    }

                    dtoClinicAddressList.Add(new DtoAddress()
                    {
                        Id = clinicAddress.Id,
                        LocalizedDtoAddresses = localizedDtoClinicAddressList,
                        ClinicPhones = dtoClinicPhoneList
                    });
                }

                var dtoSpecializationList = new List<DtoClinicSpecialization>();
                foreach (var clinicClinicSpecialization in clinic.ClinicSpecializations)
                {
                    var localizedClinicSpecializationList = new List<LocalizedDtoClinicSpecialization>();
                    foreach (var localizedClinicSpecialization in clinicClinicSpecialization.LocalizedClinicSpecializations)
                    {
                        if (localizedClinicSpecialization.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedClinicSpecializationList.Add(new LocalizedDtoClinicSpecialization()
                            {
                                Id = localizedClinicSpecialization.Id,
                                Name = localizedClinicSpecialization.Name,
                                LangCode = localizedClinicSpecialization.Language.Code
                            });
                        }
                    }

                    dtoSpecializationList.Add(new DtoClinicSpecialization()
                    {
                        Id = clinicClinicSpecialization.Id,
                        LocalizedDtoClinicSpecializations = localizedClinicSpecializationList
                    });

                }

                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Email = clinic.Email,
                    Site = clinic.Site,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Favorite = clinic.Favorite,
                    ReviewCount = clinic.ClinicReviews.Count,
                    Images = imagesList,
                    ClinicSpecialization = dtoSpecializationList,
                    ClinicAddress = dtoClinicAddressList
                };

                dtoClinicList.Add(dtoClinic);
            }

            /*
            foreach (var clinic in clinicList)
            {
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();
                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Email = clinic.Email,
                    Site = clinic.Site,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Favorite = clinic.Favorite,
                    ReviewCount = clinic.ClinicReviews.Count,
                    Images = imagesList
                };
                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicAddress = new DtoAddress
                    {
                        Country = clinicAddress.Country,
                        City = clinicAddress.City.Name,
                        Street = clinicAddress.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };
                    foreach (var clinicPhone in clinicAddress.Phones)
                    {
                        var dtoClinicPhone = new DtoPhone { Desc = clinicPhone.Description, Phone = clinicPhone.Number};
                        dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                    }
                    dtoClinicAddressList.Add(dtoClinicAddress);
                }
                dtoClinic.ClinicAddress = dtoClinicAddressList;

                var dtoSpecializationList =
                    clinic.ClinicSpecializations.Select(
                        specialization =>
                            new DtoClinicSpecialization {Id = specialization.Id, Name = specialization.Name}).ToList();
                dtoClinic.ClinicSpecialization = dtoSpecializationList;

                dtoClinicList.Add(dtoClinic);
            }
            */

            return dtoClinicList;
        }

        public DtoClinic GetClinicById(int id, string lang, string pathToImage)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }

            var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

            var dtoClinicAddressList = new List<DtoAddress>();
            foreach (var clinicAddress in clinic.CityAddresses)
            {
                var dtoClinicPhoneList = new List<DtoPhone>();
                foreach (var clinicAddressPhone in clinicAddress.Phones)
                {
                    var localizedDtoClinicPhoneList = new List<LocalizedDtoPhone>();
                    foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                    {
                        if (localizedClinicPhone.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoClinicPhoneList.Add(new LocalizedDtoPhone()
                            {
                                Id = localizedClinicPhone.Id,
                                Desc = localizedClinicPhone.Description,
                                Phone = localizedClinicPhone.Number,
                                LangCode = localizedClinicPhone.Language.Code
                            });

                        }

                    }
                    dtoClinicPhoneList.Add(new DtoPhone()
                    {
                        Id = clinicAddressPhone.Id,
                        LocalizedDtoPhones = localizedDtoClinicPhoneList
                    });
                }

                var localizedDtoClinicAddressList = new List<LocalizedDtoAddress>();
                foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses)
                {
                    if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                    {
                        localizedDtoClinicAddressList.Add(new LocalizedDtoAddress()
                        {
                            Country = clinicAddressLocalizedAddress.Country,
                            City = clinicAddressLocalizedAddress.City.LocalisedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                            Street = clinicAddressLocalizedAddress.Street,
                            LangCode = clinicAddressLocalizedAddress.Language.Code
                        });
                    }
                }

                dtoClinicAddressList.Add(new DtoAddress()
                {
                    Id = clinicAddress.Id,
                    LocalizedDtoAddresses = localizedDtoClinicAddressList,
                    ClinicPhones = dtoClinicPhoneList
                });
            }

            var dtoSpecializationList = new List<DtoClinicSpecialization>();
            foreach (var clinicClinicSpecialization in clinic.ClinicSpecializations)
            {
                var localizedClinicSpecializationList = new List<LocalizedDtoClinicSpecialization>();
                foreach (var localizedClinicSpecialization in clinicClinicSpecialization.LocalizedClinicSpecializations)
                {
                    if (localizedClinicSpecialization.Language.Code.ToLower() == lang.ToLower())
                    {
                        localizedClinicSpecializationList.Add(new LocalizedDtoClinicSpecialization()
                        {
                            Id = localizedClinicSpecialization.Id,
                            Name = localizedClinicSpecialization.Name,
                            LangCode = localizedClinicSpecialization.Language.Code
                        });
                    }
                }

                dtoSpecializationList.Add(new DtoClinicSpecialization()
                {
                    Id = clinicClinicSpecialization.Id,
                    LocalizedDtoClinicSpecializations = localizedClinicSpecializationList
                });

            }

            var dtoClinic = new DtoClinic
            {
                Id = clinic.Id,
                Email = clinic.Email,
                Site = clinic.Site,
                RatePoliteness = clinic.RatePoliteness,
                RatePrice = clinic.RatePrice,
                RateQuality = clinic.RateQuality,
                RateAverage = clinic.RateAverage,
                Favorite = clinic.Favorite,
                ReviewCount = clinic.ClinicReviews.Count,
                Images = imagesList,
                ClinicSpecialization = dtoSpecializationList,
                ClinicAddress = dtoClinicAddressList
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
            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in pagedList)
            {
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicPhoneList = new List<DtoPhone>();
                    foreach (var clinicAddressPhone in clinicAddress.Phones)
                    {
                        var localizedDtoClinicPhoneList = new List<LocalizedDtoPhone>();
                        foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                        {
                            if (localizedClinicPhone.Language.Code.ToLower() == lang.ToLower())
                            {
                                localizedDtoClinicPhoneList.Add(new LocalizedDtoPhone()
                                {
                                    Id = localizedClinicPhone.Id,
                                    Desc = localizedClinicPhone.Description,
                                    Phone = localizedClinicPhone.Number,
                                    LangCode = localizedClinicPhone.Language.Code
                                });

                            }

                        }
                        dtoClinicPhoneList.Add(new DtoPhone()
                        {
                            Id = clinicAddressPhone.Id,
                            LocalizedDtoPhones = localizedDtoClinicPhoneList
                        });
                    }

                    var localizedDtoClinicAddressList = new List<LocalizedDtoAddress>();
                    foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses)
                    {
                        if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoClinicAddressList.Add(new LocalizedDtoAddress()
                            {
                                Country = clinicAddressLocalizedAddress.Country,
                                City = clinicAddressLocalizedAddress.City.LocalisedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                                Street = clinicAddressLocalizedAddress.Street,
                                LangCode = clinicAddressLocalizedAddress.Language.Code
                            });
                        }
                    }

                    dtoClinicAddressList.Add(new DtoAddress()
                    {
                        Id = clinicAddress.Id,
                        LocalizedDtoAddresses = localizedDtoClinicAddressList,
                        ClinicPhones = dtoClinicPhoneList
                    });
                }

                var dtoSpecializationList = new List<DtoClinicSpecialization>();
                foreach (var clinicClinicSpecialization in clinic.ClinicSpecializations)
                {
                    var localizedClinicSpecializationList = new List<LocalizedDtoClinicSpecialization>();
                    foreach (var localizedClinicSpecialization in clinicClinicSpecialization.LocalizedClinicSpecializations)
                    {
                        if (localizedClinicSpecialization.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedClinicSpecializationList.Add(new LocalizedDtoClinicSpecialization()
                            {
                                Id = localizedClinicSpecialization.Id,
                                Name = localizedClinicSpecialization.Name,
                                LangCode = localizedClinicSpecialization.Language.Code
                            });
                        }
                    }

                    dtoSpecializationList.Add(new DtoClinicSpecialization()
                    {
                        Id = clinicClinicSpecialization.Id,
                        LocalizedDtoClinicSpecializations = localizedClinicSpecializationList
                    });

                }

                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Email = clinic.Email,
                    Site = clinic.Site,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Favorite = clinic.Favorite,
                    ReviewCount = clinic.ClinicReviews.Count,
                    Images = imagesList,
                    ClinicSpecialization = dtoSpecializationList,
                    ClinicAddress = dtoClinicAddressList
                };

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
            IQueryable<Clinic> clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending);
            /*
             * IQueryable<Clinic> clinics = null;
            switch (searchModel.CityId) //check whether CityId included in search request
            {
                case 0:
                    {
                        switch (searchModel.SpecializationIds.Any())
                        //check whether ClinicSpecialization included in search request
                        {
                            case true:
                                {
                                    switch (searchModel.SearchWord == "") //check whether SearchWord included in search request
                                    {
                                        case true:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(
                                                        x =>
                                                            !searchModel.SpecializationIds.Except(
                                                                x.ClinicSpecializations.Select(y => y.Id)).Any());
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(x => (x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                                !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any()) ||
                                                                (x.ClinicSpecializations.Any(z => z.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
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
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending);
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                                            x.ClinicSpecializations.Any(z => z.Name.ToLower().Contains(searchModel.SearchWord.ToLower())));
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
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(x => x.CityAddresses.Any(y => y.City.Id == searchModel.CityId) &&
                                                               !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any());
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(x => (x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.CityAddresses.Any(y => y.City.Id == searchModel.CityId) &&
                                                               !searchModel.SpecializationIds.Except(x.ClinicSpecializations.Select(y => y.Id)).Any()) ||
                                                               (x.ClinicSpecializations.Any(z => z.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.CityAddresses.Any(y => y.City.Id == searchModel.CityId) &&
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
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(x => x.CityAddresses.Any(y => y.City.Id == searchModel.CityId));
                                                break;
                                            }
                                        default:
                                            {
                                                clinics = _clinicRepository.GetSortedСlinics(searchModel.SortBy, descending).
                                                    Where(x => (x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.CityAddresses.Any(y => y.City.Id == searchModel.CityId)) ||
                                                               (x.ClinicSpecializations.Any(z => z.Name.ToLower().Contains(searchModel.SearchWord.ToLower())) &&
                                                               x.CityAddresses.Any(y => y.City.Id == searchModel.CityId)));
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
            */
            //var aaa = clinics.ToList();
            var pagedList = new PagedList<Clinic>(clinics, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }
            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in pagedList)
            {
                var imagesList = clinic.ImageName.Select(image => pathToImage + image.Name).ToList();

                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicPhoneList = new List<DtoPhone>();
                    foreach (var clinicAddressPhone in clinicAddress.Phones)
                    {
                        var localizedDtoClinicPhoneList = new List<LocalizedDtoPhone>();
                        foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                        {
                            if (localizedClinicPhone.Language.Code.ToLower() == lang.ToLower())
                            {
                                localizedDtoClinicPhoneList.Add(new LocalizedDtoPhone()
                                {
                                    Id = localizedClinicPhone.Id,
                                    Desc = localizedClinicPhone.Description,
                                    Phone = localizedClinicPhone.Number,
                                    LangCode = localizedClinicPhone.Language.Code
                                });

                            }

                        }
                        dtoClinicPhoneList.Add(new DtoPhone()
                        {
                            Id = clinicAddressPhone.Id,
                            LocalizedDtoPhones = localizedDtoClinicPhoneList
                        });
                    }

                    var localizedDtoClinicAddressList = new List<LocalizedDtoAddress>();
                    foreach (var clinicAddressLocalizedAddress in clinicAddress.LocalizedAddresses)
                    {
                        if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoClinicAddressList.Add(new LocalizedDtoAddress()
                            {
                                Country = clinicAddressLocalizedAddress.Country,
                                City = clinicAddressLocalizedAddress.City.LocalisedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                                Street = clinicAddressLocalizedAddress.Street,
                                LangCode = clinicAddressLocalizedAddress.Language.Code
                            });
                        }
                    }

                    dtoClinicAddressList.Add(new DtoAddress()
                    {
                        Id = clinicAddress.Id,
                        LocalizedDtoAddresses = localizedDtoClinicAddressList,
                        ClinicPhones = dtoClinicPhoneList
                    });
                }

                var dtoSpecializationList = new List<DtoClinicSpecialization>();
                foreach (var clinicClinicSpecialization in clinic.ClinicSpecializations)
                {
                    var localizedClinicSpecializationList = new List<LocalizedDtoClinicSpecialization>();
                    foreach (var localizedClinicSpecialization in clinicClinicSpecialization.LocalizedClinicSpecializations)
                    {
                        if (localizedClinicSpecialization.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedClinicSpecializationList.Add(new LocalizedDtoClinicSpecialization()
                            {
                                Id = localizedClinicSpecialization.Id,
                                Name = localizedClinicSpecialization.Name,
                                LangCode = localizedClinicSpecialization.Language.Code
                            });
                        }
                    }

                    dtoSpecializationList.Add(new DtoClinicSpecialization()
                    {
                        Id = clinicClinicSpecialization.Id,
                        LocalizedDtoClinicSpecializations = localizedClinicSpecializationList
                    });

                }

                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Email = clinic.Email,
                    Site = clinic.Site,
                    RatePoliteness = clinic.RatePoliteness,
                    RatePrice = clinic.RatePrice,
                    RateQuality = clinic.RateQuality,
                    RateAverage = clinic.RateAverage,
                    Favorite = clinic.Favorite,
                    ReviewCount = clinic.ClinicReviews.Count,
                    Images = imagesList,
                    ClinicSpecialization = dtoSpecializationList,
                    ClinicAddress = dtoClinicAddressList
                };

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
            _searchService.RefreshCache();
        }

        public void Update(int id, string name)
        {
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