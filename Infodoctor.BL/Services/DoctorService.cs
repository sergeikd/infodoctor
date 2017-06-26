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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorCategoryRepository _doctorCategoryRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ISearchService _searchService;
        private readonly ILanguageRepository _languageRepository;

        public DoctorService(IDoctorRepository doctorRepository,
            IDoctorCategoryRepository doctorCategoryRepository,
            IClinicRepository clinicRepository,
            ISearchService searchService, ILanguageRepository languageRepository)
        {
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (doctorCategoryRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _searchService = searchService;
            _languageRepository = languageRepository;
            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
            _doctorCategoryRepository = doctorCategoryRepository;
        }

        public IEnumerable<DtoDoctorSingleLang> GetAllDoctors(string pathToImage, string lang)
        {
            var doctors = _doctorRepository.GetAllDoctors().ToList();
            var result = new List<DtoDoctorSingleLang>();

            foreach (var doctor in doctors)
            {
                var local = new DtoDoctorLocalized();

                if (doctor.Localized != null)
                    foreach (var localizedDoctor in doctor.Localized)
                        if (string.Equals(localizedDoctor.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase))
                        {
                            local = new DtoDoctorLocalized()
                            {
                                Id = localizedDoctor.Id,
                                Name = localizedDoctor.Name,
                                Specialization = localizedDoctor.Specialization,
                                Manipulation = localizedDoctor.Manipulation,
                                LangCode = localizedDoctor.Language.Code.ToLower()
                            };
                        }

                var category = new DtoDoctorCategorySingleLang();
                if (doctor.Category.Localized != null)
                    foreach (var dc in doctor.Category.Localized)
                        if (string.Equals(dc.Language.Code, lang, StringComparison.InvariantCultureIgnoreCase))
                            category = new DtoDoctorCategorySingleLang()
                            {
                                Id = dc.Id,
                                Name = dc.Name,
                                LangCode = dc.Language.Code
                            };

                var dtoDoctor = new DtoDoctorSingleLang()
                {
                    Id = doctor.Id,
                    LangCode = local.LangCode.ToLower(),
                    Name = local.Name,
                    Manipulation = local.Manipulation,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Specialization = local.Specialization,
                    Category = category,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    ReviewCount = doctor.DoctorReviews.Count,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoPhoneList = new List<DtoPhoneSingleLang>();
                    foreach (var clinicAddressPhone in doctor.Address.Phones)
                    {
                        var localizedDtoPhone = new LocalizedDtoPhone();
                        foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                        {
                            if (string.Equals(localizedClinicPhone.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                            {
                                localizedDtoPhone = new LocalizedDtoPhone()
                                {
                                    Id = localizedClinicPhone.Id,
                                    Desc = localizedClinicPhone.Description,
                                    Number = localizedClinicPhone.Number,
                                    LangCode = localizedClinicPhone.Language.Code
                                };

                            }

                        }
                        dtoPhoneList.Add(new DtoPhoneSingleLang()
                        {
                            Id = clinicAddressPhone.Id,
                            Desc = localizedDtoPhone.Desc,
                            Number = localizedDtoPhone.Number
                        });
                    }

                    var localizedDtoAddress = new LocalizedDtoAddress();
                    foreach (var clinicAddressLocalizedAddress in doctor.Address.LocalizedAddresses)
                    {
                        if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoAddress = new LocalizedDtoAddress()
                            {
                                Country = clinicAddressLocalizedAddress.Country,
                                City = clinicAddressLocalizedAddress.City.LocalizedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                                Street = clinicAddressLocalizedAddress.Street,
                                LangCode = clinicAddressLocalizedAddress.Language.Code.ToLower()
                            };
                        }
                    }

                    var dtoAddress = new DtoAddressSingleLang()
                    {
                        Id = doctor.Address.Id,
                        Country = localizedDtoAddress.Country,
                        City = localizedDtoAddress.City,
                        Street = localizedDtoAddress.Street,
                        Phones = dtoPhoneList
                    };

                    dtoDoctor.Address = dtoAddress;
                }

                if (doctor.Clinics != null)
                {
                    dtoDoctor.ClinicsIds = new List<int>();
                    foreach (var clinic in doctor.Clinics)
                    {
                        dtoDoctor.ClinicsIds.Add(clinic.Id);
                    }
                }
                result.Add(dtoDoctor);
            }

            return result;
        }

        public DtoPagedDoctor GetPagedDoctors(int perPage, int numPage, string pathToImage, string lang)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }

            var doctors = _doctorRepository.GetAllDoctors();
            var pagedList = new PagedList<Doctor>(doctors, perPage, numPage);
            if (!pagedList.Any())
            {
                throw new ApplicationException("Page not found");
            }

            var dtoDoctors = new List<DtoDoctorSingleLang>();

            foreach (var doctor in pagedList)
            {
                var local = new DtoDoctorLocalized();

                if (doctor.Localized != null)
                    foreach (var localizedDoctor in doctor.Localized)
                        if (string.Equals(localizedDoctor.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase))
                        {
                            local = new DtoDoctorLocalized()
                            {
                                Id = localizedDoctor.Id,
                                Name = localizedDoctor.Name,
                                Specialization = localizedDoctor.Specialization,
                                Manipulation = localizedDoctor.Manipulation,
                                LangCode = localizedDoctor.Language.Code.ToLower()
                            };
                        }

                var category = new DtoDoctorCategorySingleLang();
                if (doctor.Category.Localized != null)
                    foreach (var dc in doctor.Category.Localized)
                        if (string.Equals(dc.Language.Code, lang, StringComparison.InvariantCultureIgnoreCase))
                            category = new DtoDoctorCategorySingleLang()
                            {
                                Id = dc.Id,
                                Name = dc.Name,
                                LangCode = dc.Language.Code.ToLower()
                            };

                var dtoDoctor = new DtoDoctorSingleLang()
                {
                    Id = doctor.Id,
                    LangCode = local.LangCode.ToLower(),
                    Name = local.Name,
                    Manipulation = local.Manipulation,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Specialization = local.Specialization,
                    Category = category,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    ReviewCount = doctor.DoctorReviews.Count,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoPhoneList = new List<DtoPhoneSingleLang>();
                    foreach (var clinicAddressPhone in doctor.Address.Phones)
                    {
                        var localizedDtoPhone = new LocalizedDtoPhone();
                        foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                        {
                            if (string.Equals(localizedClinicPhone.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                            {
                                localizedDtoPhone = new LocalizedDtoPhone()
                                {
                                    Id = localizedClinicPhone.Id,
                                    Desc = localizedClinicPhone.Description,
                                    Number = localizedClinicPhone.Number,
                                    LangCode = localizedClinicPhone.Language.Code.ToLower()
                                };

                            }

                        }
                        dtoPhoneList.Add(new DtoPhoneSingleLang()
                        {
                            Id = clinicAddressPhone.Id,
                            Desc = localizedDtoPhone.Desc,
                            Number = localizedDtoPhone.Number
                        });
                    }

                    var localizedDtoAddress = new LocalizedDtoAddress();
                    foreach (var clinicAddressLocalizedAddress in doctor.Address.LocalizedAddresses)
                    {
                        if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoAddress = new LocalizedDtoAddress()
                            {
                                Country = clinicAddressLocalizedAddress.Country,
                                City = clinicAddressLocalizedAddress.City.LocalizedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                                Street = clinicAddressLocalizedAddress.Street,
                                LangCode = clinicAddressLocalizedAddress.Language.Code.ToLower()
                            };
                        }
                    }

                    var dtoAddress = new DtoAddressSingleLang()
                    {
                        Id = doctor.Address.Id,
                        Country = localizedDtoAddress.Country,
                        City = localizedDtoAddress.City,
                        Street = localizedDtoAddress.Street,
                        Phones = dtoPhoneList
                    };

                    dtoDoctor.Address = dtoAddress;
                }

                if (doctor.Clinics != null)
                {
                    dtoDoctor.ClinicsIds = new List<int>();
                    foreach (var clinic in doctor.Clinics)
                    {
                        dtoDoctor.ClinicsIds.Add(clinic.Id);
                    }
                }
                dtoDoctors.Add(dtoDoctor);
            }

            var pagedDtoDoclorList = new DtoPagedDoctor()
            {
                Doctors = dtoDoctors,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };

            return pagedDtoDoclorList;
        }

        public DtoPagedDoctor SearchDoctors(int perPage, int numPage, DtoDoctorSearchModel searchModel,
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

            IQueryable<Doctor> doctors;

            switch (searchModel.CityId) //check whether CityId included in search request
            {
                case 0:
                    {
                        switch (searchModel.SearchWord == "")
                        {
                            case true:
                                {
                                    doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending, lang);
                                    break;
                                }
                            default:
                                {
                                    doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending, lang).
                                        Where(
                                            x => x.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                            x.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Specialization.ToLower().Contains(searchModel.SearchWord.ToLower())
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
                                    doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending, lang).Where(x => x.Address.Id == searchModel.CityId);
                                    break;
                                }
                            default:
                                {
                                    doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending, lang).
                                        Where(
                                            x => x.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name.ToLower().Contains(searchModel.SearchWord.ToLower()) ||
                                            x.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Specialization.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                            x.Address.Id == searchModel.CityId
                                        );
                                    break;
                                }
                        }
                        break;
                    }
            }

            var pagedList = new PagedList<Doctor>(doctors, perPage, numPage);

            if (!pagedList.Any())
                return null;

            var dtoDoctors = new List<DtoDoctorSingleLang>();

            foreach (var doctor in pagedList)
            {
                var local = new DtoDoctorLocalized();

                if (doctor.Localized != null)
                    foreach (var localizedDoctor in doctor.Localized)
                        if (string.Equals(localizedDoctor.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase))
                        {
                            local = new DtoDoctorLocalized()
                            {
                                Id = localizedDoctor.Id,
                                Name = localizedDoctor.Name,
                                Specialization = localizedDoctor.Specialization,
                                Manipulation = localizedDoctor.Manipulation,
                                LangCode = localizedDoctor.Language.Code.ToLower()
                            };
                        }

                var category = new DtoDoctorCategorySingleLang();
                if (doctor.Category.Localized != null)
                    foreach (var dc in doctor.Category.Localized)
                        if (string.Equals(dc.Language.Code, lang, StringComparison.InvariantCultureIgnoreCase))
                            category = new DtoDoctorCategorySingleLang()
                            {
                                Id = dc.Id,
                                Name = dc.Name,
                                LangCode = dc.Language.Code.ToLower()
                            };

                var dtoDoctor = new DtoDoctorSingleLang()
                {
                    Id = doctor.Id,
                    LangCode = local.LangCode.ToLower(),
                    Name = local.Name,
                    Manipulation = local.Manipulation,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Specialization = local.Specialization,
                    Category = category,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    ReviewCount = doctor.DoctorReviews.Count,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoPhoneList = new List<DtoPhoneSingleLang>();
                    foreach (var clinicAddressPhone in doctor.Address.Phones)
                    {
                        var localizedDtoPhone = new LocalizedDtoPhone();
                        foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                        {
                            if (string.Equals(localizedClinicPhone.Language.Code.ToLower(), lang.ToLower(),
                                StringComparison.Ordinal))
                            {
                                localizedDtoPhone = new LocalizedDtoPhone()
                                {
                                    Id = localizedClinicPhone.Id,
                                    Desc = localizedClinicPhone.Description,
                                    Number = localizedClinicPhone.Number,
                                    LangCode = localizedClinicPhone.Language.Code.ToLower()
                                };

                            }

                        }
                        dtoPhoneList.Add(new DtoPhoneSingleLang()
                        {
                            Id = clinicAddressPhone.Id,
                            Desc = localizedDtoPhone.Desc,
                            Number = localizedDtoPhone.Number
                        });
                    }

                    var localizedDtoAddress = new LocalizedDtoAddress();
                    foreach (var clinicAddressLocalizedAddress in doctor.Address.LocalizedAddresses)
                    {
                        if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                        {
                            localizedDtoAddress = new LocalizedDtoAddress()
                            {
                                Country = clinicAddressLocalizedAddress.Country,
                                City = clinicAddressLocalizedAddress.City.LocalizedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                                Street = clinicAddressLocalizedAddress.Street,
                                LangCode = clinicAddressLocalizedAddress.Language.Code.ToLower()
                            };
                        }
                    }

                    var dtoAddress = new DtoAddressSingleLang()
                    {
                        Id = doctor.Address.Id,
                        Country = localizedDtoAddress.Country,
                        City = localizedDtoAddress.City,
                        Street = localizedDtoAddress.Street,
                        Phones = dtoPhoneList
                    };

                    dtoDoctor.Address = dtoAddress;
                }

                if (doctor.Clinics != null)
                {
                    dtoDoctor.ClinicsIds = new List<int>();
                    foreach (var clinic in doctor.Clinics)
                    {
                        dtoDoctor.ClinicsIds.Add(clinic.Id);
                    }
                }
                dtoDoctors.Add(dtoDoctor);
            }

            var pagedDtoDoclorList = new DtoPagedDoctor()
            {
                Doctors = dtoDoctors,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };

            return pagedDtoDoclorList;
        }

        public DtoDoctorSingleLang GetDoctorById(int id, string pathToImage, string lang)
        {
            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor == null)
                throw new ApplicationException("Doctor not found");

            var local = new DtoDoctorLocalized();

            if (doctor.Localized != null)
                foreach (var localizedDoctor in doctor.Localized)
                    if (string.Equals(localizedDoctor.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase))
                    {
                        local = new DtoDoctorLocalized()
                        {
                            Id = localizedDoctor.Id,
                            Name = localizedDoctor.Name,
                            Specialization = localizedDoctor.Specialization,
                            Manipulation = localizedDoctor.Manipulation,
                            LangCode = localizedDoctor.Language.Code.ToLower()
                        };
                    }

            var category = new DtoDoctorCategorySingleLang();
            if (doctor.Category.Localized != null)
                foreach (var dc in doctor.Category.Localized)
                    if (string.Equals(dc.Language.Code, lang, StringComparison.InvariantCultureIgnoreCase))
                        category = new DtoDoctorCategorySingleLang()
                        {
                            Id = dc.Id,
                            Name = dc.Name,
                            LangCode = dc.Language.Code.ToLower()
                        };

            var dtoDoctor = new DtoDoctorSingleLang()
            {
                Id = doctor.Id,
                LangCode = local.LangCode.ToLower(),
                Name = local.Name,
                Manipulation = local.Manipulation,
                Email = doctor.Email,
                Experience = doctor.Experience,
                Specialization = local.Specialization,
                Category = category,
                RatePoliteness = doctor.RatePoliteness,
                RateProfessionalism = doctor.RateProfessionalism,
                RateWaitingTime = doctor.RateWaitingTime,
                RateAverage = doctor.RateAverage,
                ReviewCount = doctor.DoctorReviews.Count,
                Image = pathToImage + doctor.ImageName
            };

            if (doctor.Address != null)
            {
                var dtoPhoneList = new List<DtoPhoneSingleLang>();
                foreach (var clinicAddressPhone in doctor.Address.Phones)
                {
                    var localizedDtoPhone = new LocalizedDtoPhone();
                    foreach (var localizedClinicPhone in clinicAddressPhone.LocalizedPhones)
                    {
                        if (string.Equals(localizedClinicPhone.Language.Code.ToLower(), lang.ToLower(),
                            StringComparison.Ordinal))
                        {
                            localizedDtoPhone = new LocalizedDtoPhone()
                            {
                                Id = localizedClinicPhone.Id,
                                Desc = localizedClinicPhone.Description,
                                Number = localizedClinicPhone.Number,
                                LangCode = localizedClinicPhone.Language.Code.ToLower()
                            };

                        }

                    }
                    dtoPhoneList.Add(new DtoPhoneSingleLang()
                    {
                        Id = clinicAddressPhone.Id,
                        Desc = localizedDtoPhone.Desc,
                        Number = localizedDtoPhone.Number
                    });
                }

                var localizedDtoAddress = new LocalizedDtoAddress();
                foreach (var clinicAddressLocalizedAddress in doctor.Address.LocalizedAddresses)
                {
                    if (clinicAddressLocalizedAddress.Language.Code.ToLower() == lang.ToLower())
                    {
                        localizedDtoAddress = new LocalizedDtoAddress()
                        {
                            Country = clinicAddressLocalizedAddress.Country,
                            City = clinicAddressLocalizedAddress.City.LocalizedCities.First(c => c.Language.Code.ToLower() == lang.ToLower()).Name,
                            Street = clinicAddressLocalizedAddress.Street,
                            LangCode = clinicAddressLocalizedAddress.Language.Code.ToLower()
                        };
                    }
                }

                var dtoAddress = new DtoAddressSingleLang()
                {
                    Id = doctor.Address.Id,
                    Country = localizedDtoAddress.Country,
                    City = localizedDtoAddress.City,
                    Street = localizedDtoAddress.Street,
                    Phones = dtoPhoneList
                };

                dtoDoctor.Address = dtoAddress;
            }

            if (doctor.Clinics != null)
            {
                dtoDoctor.ClinicsIds = new List<int>();
                foreach (var clinic in doctor.Clinics)
                {
                    dtoDoctor.ClinicsIds.Add(clinic.Id);
                }
            }
            return dtoDoctor;
        }

        public void Add(DtoDoctorMultiLang newDoctorMultiLang)
        {
            if (newDoctorMultiLang == null)
                throw new ArgumentNullException(nameof(newDoctorMultiLang));

            var locals = new List<LocalizedDoctor>();

            if (newDoctorMultiLang.Localized != null)
                foreach (var doctorLocalized in newDoctorMultiLang.Localized)
                {
                    locals.Add(new LocalizedDoctor() { Name = doctorLocalized.Name, Manipulation = doctorLocalized.Manipulation, Language = _languageRepository.GetLanguageByCode(doctorLocalized.LangCode), Specialization = doctorLocalized.Specialization });
                }

            var doctor = new Doctor()
            {
                Localized = locals,
                Email = newDoctorMultiLang.Email,
                Experience = newDoctorMultiLang.Experience,
                ImageName = newDoctorMultiLang.Image
            };

            var clinicsList = new List<Clinic>();

            foreach (var clinicId in newDoctorMultiLang.ClinicsIds)
            {
                var clinic = _clinicRepository.GetClinicById(clinicId);
                if (clinic != null)
                    clinicsList.Add(clinic);
            }

            doctor.Clinics = clinicsList;

            var doctorCategoryList = _doctorCategoryRepository.GetAllCategories().ToList();

            var category = new DoctorCategory();
            if (newDoctorMultiLang.Category.Localized.Any())
            {
                var local = newDoctorMultiLang.Category.Localized[0];
                category = doctorCategoryList.First(dc =>
                {
                    var localizedDC = dc.Localized.FirstOrDefault(l => string.Equals(l.Language.Code, local.LangCode,
                        StringComparison.CurrentCultureIgnoreCase));
                    return localizedDC != null && string.Equals(localizedDC.Name, local.Name,
                               StringComparison.CurrentCultureIgnoreCase);
                });
            }

            doctor.Category = category;


            _doctorRepository.Add(doctor);
            _searchService.RefreshCache();
        }

        public void Update(int id, DtoDoctorMultiLang newDoctorMultiLang)
        {
            if (newDoctorMultiLang == null)
                throw new ArgumentNullException(nameof(newDoctorMultiLang));

            var locals = new List<LocalizedDoctor>();

            if (newDoctorMultiLang.Localized != null)
                foreach (var doctorLocalized in newDoctorMultiLang.Localized)
                {
                    locals.Add(new LocalizedDoctor() { Name = doctorLocalized.Name, Manipulation = doctorLocalized.Manipulation, Language = _languageRepository.GetLanguageByCode(doctorLocalized.LangCode), Specialization = doctorLocalized.Specialization });
                }

            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor == null)
                throw new ApplicationException($"Doctor with id={id} not found");

            doctor.Localized = locals;
            doctor.Email = newDoctorMultiLang.Email;
            doctor.Experience = newDoctorMultiLang.Experience;
            doctor.ImageName = newDoctorMultiLang.Image;

            var doctorCategoryList = _doctorCategoryRepository.GetAllCategories().ToList();
            var clinicsList = new List<Clinic>();

            foreach (var clinicId in newDoctorMultiLang.ClinicsIds)
            {
                var clinic = _clinicRepository.GetClinicById(clinicId);
                if (clinic != null)
                    clinicsList.Add(clinic);
            }

            var category = new DoctorCategory();
            if (newDoctorMultiLang.Category.Localized.Any())
            {
                var local = newDoctorMultiLang.Category.Localized[0];
                category = doctorCategoryList.First(dc =>
                {
                    var localizedDC = dc.Localized.FirstOrDefault(l => string.Equals(l.Language.Code, local.LangCode,
                        StringComparison.CurrentCultureIgnoreCase));
                    return localizedDC != null && string.Equals(localizedDC.Name, local.Name,
                               StringComparison.CurrentCultureIgnoreCase);
                });
            }

            doctor.Clinics = clinicsList;
            doctor.Category = category;

            _doctorRepository.Update(doctor);
            _searchService.RefreshCache();
        }

        public void Delete(int id)
        {
            var doctor = _doctorRepository.GetDoctorById(id);
            if (doctor != null)
            {
                _doctorRepository.Delete(doctor);
                _searchService.RefreshCache();
            }
        }
    }
}
