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
        private readonly IDoctorSpecializationRepository _doctorSpecializationRepository;
        private readonly IDoctorCategoryRepository _doctorCategoryRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ISearchService _searchService;

        public DoctorService(IDoctorRepository doctorRepository,
            IDoctorSpecializationRepository doctorSpecializationRepository,
            IDoctorCategoryRepository doctorCategoryRepository,
            IClinicRepository clinicRepository,
            ISearchService searchService)
        {
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));
            if (doctorCategoryRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));

            _searchService = searchService;
            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
            _doctorSpecializationRepository = doctorSpecializationRepository;
            _doctorCategoryRepository = doctorCategoryRepository;
        }

        public IEnumerable<DtoDoctor> GetAllDoctors(string pathToImage, string lang)
        {
            var doctors = _doctorRepository.GetAllDoctors().ToList();
            var result = new List<DtoDoctor>();

            foreach (var doctor in doctors)
            {
                var dtoDoctor = new DtoDoctor()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Manipulation = doctor.Manipulation,
                    Specialization = new DtoDoctorSpecialization() { Id = doctor.Specialization.Id, Name = doctor.Specialization.Name },
                    Category = doctor.Category.Name,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    ReviewCount = doctor.DoctorReviews.Count,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoPhoneList = new List<DtoPhoneMultiLang>();
                    foreach (var clinicAddressPhone in doctor.Address.Phones)
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
                        dtoPhoneList.Add(new DtoPhoneMultiLang()
                        {
                            Id = clinicAddressPhone.Id,
                            LocalizedDtoPhones = localizedDtoClinicPhoneList
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
                                LangCode = clinicAddressLocalizedAddress.Language.Code
                            };
                        }
                    }

                    var dtoAddress = new DtoAddressMultiLang()
                    {
                        Id = doctor.Address.Id,
                        LocalizedDtoAddresses = new List<LocalizedDtoAddress>() { localizedDtoAddress },
                        Phones = dtoPhoneList
                    };

                    dtoDoctor.AddressMultilang = dtoAddress;
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

            var dtoDoctors = new List<DtoDoctor>();

            foreach (var doctor in pagedList)
            {
                var dtoDoctor = new DtoDoctor()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Manipulation = doctor.Manipulation,
                    Specialization = new DtoDoctorSpecialization() { Id = doctor.Specialization.Id, Name = doctor.Specialization.Name },
                    Category = doctor.Category.Name,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    ReviewCount = doctor.DoctorReviews.Count,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoPhoneList = new List<DtoPhoneMultiLang>();
                    foreach (var clinicAddressPhone in doctor.Address.Phones)
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
                        dtoPhoneList.Add(new DtoPhoneMultiLang()
                        {
                            Id = clinicAddressPhone.Id,
                            LocalizedDtoPhones = localizedDtoClinicPhoneList
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
                                LangCode = clinicAddressLocalizedAddress.Language.Code
                            };
                        }
                    }

                    var dtoAddress = new DtoAddressMultiLang()
                    {
                        Id = doctor.Address.Id,
                        LocalizedDtoAddresses = new List<LocalizedDtoAddress>() { localizedDtoAddress },
                        Phones = dtoPhoneList
                    };

                    dtoDoctor.AddressMultilang = dtoAddress;
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
                ;
            }
            IQueryable<Doctor> doctors;
            switch (searchModel.CityId) //check whether CityId included in search request
            {
                case 0:
                    {
                        switch (searchModel.SpecializationId != 0) //check whether DoctorSpecialization included in search request
                        {
                            case true:
                                {
                                    switch (searchModel.SearchWord == "") //check whether SearchWord included in search request
                                    {
                                        case true:
                                            {
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                    Where(x => (x.Specialization.Id == searchModel.SpecializationId));
                                                break;
                                            }
                                        default:
                                            {
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                    Where(x => (x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                                (x.Specialization.Id == searchModel.SpecializationId)));
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
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending);
                                                break;
                                            }
                                        default:
                                            {
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                    Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()));
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
                        switch (searchModel.SpecializationId == 0) //check whether DoctorSpecialization included in search request
                        {
                            case true:
                                {
                                    switch (searchModel.SearchWord == "")
                                    {
                                        case true:
                                            {
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                               Where(x => (x.Address.Id == searchModel.CityId) &&
                                                               (x.Specialization.Id == searchModel.SpecializationId));
                                                break;
                                            }
                                        default:
                                            {
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                    Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               (x.Address.Id == searchModel.CityId) &&
                                                               (x.Specialization.Id == searchModel.SpecializationId));
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
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                    Where(x => x.Address.Id == searchModel.CityId);
                                                break;
                                            }
                                        default:
                                            {
                                                doctors = _doctorRepository.GetSortedDoctors(searchModel.SortBy, descending).
                                                    Where(x => x.Name.ToLower().Contains(searchModel.SearchWord.ToLower()) &&
                                                               x.Address.Id == searchModel.CityId);
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }

            var pagedList = new PagedList<Doctor>(doctors, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }
            var dtoDoctors = new List<DtoDoctor>();

            foreach (var doctor in pagedList)
            {
                var dtoDoctor = new DtoDoctor()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Manipulation = doctor.Manipulation,
                    Specialization = new DtoDoctorSpecialization() { Id = doctor.Specialization.Id, Name = doctor.Specialization.Name },
                    Category = doctor.Category.Name,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    ReviewCount = doctor.DoctorReviews.Count,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoPhoneList = new List<DtoPhoneMultiLang>();
                    foreach (var clinicAddressPhone in doctor.Address.Phones)
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
                        dtoPhoneList.Add(new DtoPhoneMultiLang()
                        {
                            Id = clinicAddressPhone.Id,
                            LocalizedDtoPhones = localizedDtoClinicPhoneList
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
                                LangCode = clinicAddressLocalizedAddress.Language.Code
                            };
                        }
                    }

                    var dtoAddress = new DtoAddressMultiLang()
                    {
                        Id = doctor.Address.Id,
                        LocalizedDtoAddresses = new List<LocalizedDtoAddress>() { localizedDtoAddress },
                        Phones = dtoPhoneList
                    };

                    dtoDoctor.AddressMultilang = dtoAddress;
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

        public DtoDoctor GetDoctorById(int id, string pathToImage, string lang)
        {
            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor == null)
                throw new ApplicationException("Doctor not found");

            var dtoDoctor = new DtoDoctor()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Email = doctor.Email,
                Experience = doctor.Experience,
                Manipulation = doctor.Manipulation,
                Specialization = new DtoDoctorSpecialization() { Id = doctor.Specialization.Id, Name = doctor.Specialization.Name },
                Category = doctor.Category.Name,
                RatePoliteness = doctor.RatePoliteness,
                RateProfessionalism = doctor.RateProfessionalism,
                RateWaitingTime = doctor.RateWaitingTime,
                RateAverage = doctor.RateAverage,
                ReviewCount = doctor.DoctorReviews.Count,
                Image = pathToImage + doctor.ImageName
            };

            if (doctor.Address != null)
            {
                var dtoPhoneList = new List<DtoPhoneMultiLang>();
                foreach (var clinicAddressPhone in doctor.Address.Phones)
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
                    dtoPhoneList.Add(new DtoPhoneMultiLang()
                    {
                        Id = clinicAddressPhone.Id,
                        LocalizedDtoPhones = localizedDtoClinicPhoneList
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
                            LangCode = clinicAddressLocalizedAddress.Language.Code
                        };
                    }
                }

                var dtoAddress = new DtoAddressMultiLang()
                {
                    Id = doctor.Address.Id,
                    LocalizedDtoAddresses = new List<LocalizedDtoAddress>() { localizedDtoAddress },
                    Phones = dtoPhoneList
                };

                dtoDoctor.AddressMultilang = dtoAddress;
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

        public void Add(DtoDoctor newDoctor)
        {
            if (newDoctor == null)
                throw new ArgumentNullException(nameof(newDoctor));

            var doctor = new Doctor()
            {
                Name = newDoctor.Name,
                Email = newDoctor.Email,
                Experience = newDoctor.Experience,
                Manipulation = newDoctor.Manipulation,
                ImageName = newDoctor.Image
            };

            var clinicsList = new List<Clinic>();

            foreach (var clinicId in newDoctor.ClinicsIds)
            {
                var clinic = _clinicRepository.GetClinicById(clinicId);
                if (clinic != null)
                    clinicsList.Add(clinic);
            }

            doctor.Clinics = clinicsList;

            var doctorSpesList =
                _doctorSpecializationRepository.GetAllSpecializations().ToList();
            var doctorCategoryList = _doctorCategoryRepository.GetAllCategories().ToList();

            doctor.Specialization = doctorSpesList.First(ds => String.Equals(ds.Name, newDoctor.Specialization.Name, StringComparison.CurrentCultureIgnoreCase));
            doctor.Category = doctorCategoryList.First(dc => String.Equals(dc.Name, newDoctor.Category, StringComparison.CurrentCultureIgnoreCase));


            _doctorRepository.Add(doctor);
            _searchService.RefreshCache();
        }

        public void Update(int id, DtoDoctor newDoctor)
        {
            if (newDoctor == null)
                throw new ArgumentNullException(nameof(newDoctor));

            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor != null)
            {
                doctor.Name = newDoctor.Name;
                doctor.Email = newDoctor.Email;
                doctor.Experience = newDoctor.Experience;
                doctor.Manipulation = newDoctor.Manipulation;
                doctor.ImageName = newDoctor.Image;

                var doctorSpesList =
                    _doctorSpecializationRepository.GetAllSpecializations().ToList();
                var doctorCategotyList = _doctorCategoryRepository.GetAllCategories().ToList();
                var clinicsList = new List<Clinic>();

                foreach (var clinicId in newDoctor.ClinicsIds)
                {
                    var clinic = _clinicRepository.GetClinicById(clinicId);
                    if (clinic != null)
                        clinicsList.Add(clinic);
                }

                doctor.Clinics = clinicsList;
                doctor.Specialization = doctorSpesList.First(ds => string.Equals(ds.Name, newDoctor.Specialization.Name, StringComparison.CurrentCultureIgnoreCase));
                doctor.Category = doctorCategotyList.First(dc => string.Equals(dc.Name, newDoctor.Category, StringComparison.CurrentCultureIgnoreCase));

                _doctorRepository.Update(doctor);
                _searchService.RefreshCache();
            }
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
