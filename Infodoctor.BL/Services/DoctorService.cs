using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
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
        private readonly IСlinicRepository _clinicRepository;
        private readonly ISearchService _searchService;

        public DoctorService(IDoctorRepository doctorRepository, 
            IDoctorSpecializationRepository doctorSpecializationRepository,
            IDoctorCategoryRepository doctorCategoryRepository, 
            IСlinicRepository clinicRepository, 
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

        public IEnumerable<DtoDoctor> GetAllDoctors(string pathToImage)
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
                    Specialization = doctor.Specialization.Name,
                    Category = doctor.Category.Name,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoAddress = new DtoAddress()
                    {
                        City = doctor.Address.City.Name,
                        Street = doctor.Address.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };

                    foreach (var phone in doctor.Address.ClinicPhones)
                    {
                        var dtoPhone = new DtoPhone() { Desc = phone.Description, Phone = phone.Number };
                        dtoAddress.ClinicPhones.Add(dtoPhone);
                    }
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

        public DtoPagedDoctor GetPagedDoctors(int perPage, int numPage, string pathToImage)
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
                    Specialization = doctor.Specialization.Name,
                    Category = doctor.Category.Name,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoAddress = new DtoAddress()
                    {
                        City = doctor.Address.City.Name,
                        Street = doctor.Address.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };

                    foreach (var phone in doctor.Address.ClinicPhones)
                    {
                        var dtoPhone = new DtoPhone() { Desc = phone.Description, Phone = phone.Number };
                        dtoAddress.ClinicPhones.Add(dtoPhone);
                    }
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
            string pathToImage)
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
                    Specialization = doctor.Specialization.Name,
                    Category = doctor.Category.Name,
                    RatePoliteness = doctor.RatePoliteness,
                    RateProfessionalism = doctor.RateProfessionalism,
                    RateWaitingTime = doctor.RateWaitingTime,
                    RateAverage = doctor.RateAverage,
                    Image = pathToImage + doctor.ImageName
                };

                if (doctor.Address != null)
                {
                    var dtoAddress = new DtoAddress()
                    {
                        City = doctor.Address.City.Name,
                        Street = doctor.Address.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };

                    foreach (var phone in doctor.Address.ClinicPhones)
                    {
                        var dtoPhone = new DtoPhone() { Desc = phone.Description, Phone = phone.Number };
                        dtoAddress.ClinicPhones.Add(dtoPhone);
                    }
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

        public DtoDoctor GetDoctorById(int id, string pathToImage)
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
                Specialization = doctor.Specialization.Name,
                Category = doctor.Category.Name,
                RatePoliteness = doctor.RatePoliteness,
                RateProfessionalism = doctor.RateProfessionalism,
                RateWaitingTime = doctor.RateWaitingTime,
                RateAverage = doctor.RateAverage,
                Image = pathToImage + doctor.ImageName
            };

            if (doctor.Address != null)
            {
                var dtoAddress = new DtoAddress()
                {
                    City = doctor.Address.City.Name,
                    Street = doctor.Address.Street,
                    ClinicPhones = new List<DtoPhone>()
                };

                foreach (var phone in doctor.Address.ClinicPhones)
                {
                    var dtoPhone = new DtoPhone() { Desc = phone.Description, Phone = phone.Number };
                    dtoAddress.ClinicPhones.Add(dtoPhone);
                }
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
            var doctorCategotyList = _doctorCategoryRepository.GetAllCategories().ToList();

            doctor.Specialization = doctorSpesList.First(ds => String.Equals(ds.Name, newDoctor.Specialization, StringComparison.CurrentCultureIgnoreCase));
            doctor.Category = doctorCategotyList.First(dc => String.Equals(dc.Name, newDoctor.Category, StringComparison.CurrentCultureIgnoreCase));


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
                doctor.Specialization = doctorSpesList.First(ds => string.Equals(ds.Name, newDoctor.Specialization, StringComparison.CurrentCultureIgnoreCase));
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
