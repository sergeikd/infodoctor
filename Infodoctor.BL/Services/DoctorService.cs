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
        private readonly ISearchService _searchService;

        public DoctorService(IDoctorRepository doctorRepository,
            ISearchService searchService, ILanguageRepository languageRepository)
        {
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _searchService = searchService;
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<DtoDoctorSingleLang> GetAllDoctors(string pathToImage, string lang)
        {
            var doctors = _doctorRepository.GetAllDoctors().ToList();
            var result = new List<DtoDoctorSingleLang>();

            foreach (var doctor in doctors)
            {
                var dtoDoctor = ConvertEntityToDtoSingleLang(lang, pathToImage, doctor);
                result.Add(dtoDoctor);
            }

            return result;
        }

        public DtoPagedDoctor GetPagedDoctors(int perPage, int numPage, string pathToImage, string lang)
        {
            if (perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var doctors = _doctorRepository.GetAllDoctors();
            var pagedList = new PagedList<Doctor>(doctors, perPage, numPage);
            if (!pagedList.Any())
            {
                throw new ApplicationException("Page not found");
            }

            var dtoDoctors = new List<DtoDoctorSingleLang>();

            foreach (var doctor in pagedList)
            {
                var dtoDoctor = ConvertEntityToDtoSingleLang(lang, pathToImage, doctor);
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
                var dtoDoctor = ConvertEntityToDtoSingleLang(lang, pathToImage, doctor);
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

            var dtoDoctor = ConvertEntityToDtoSingleLang(lang, pathToImage, doctor);

            return dtoDoctor;
        }

        public DtoDoctorMultiLang GetDoctorById(int id, string pathToImage)
        {
            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor == null)
                throw new ApplicationException("Doctor not found");

            var address = doctor.Address;
            var coords = new Coords()
            {
                Lat = address.Lat,
                Lng = address.Lng
            };

            var localAddresses = new List<LocalizedDtoAddress>();
            foreach (var local in address.LocalizedAddresses)
            {
                var localizedCountry = new LocalizedCountry();
                foreach (var country in address.Country.LocalizedCountries)
                    if (string.Equals(country.Language.Code.ToLower(), local.Language.Code.ToLower(),
                        StringComparison.Ordinal))
                        localizedCountry = country;

                var localizedCity = new LocalizedCity();
                foreach (var city in address.City.LocalizedCities)
                    if (string.Equals(city.Language.Code.ToLower(), local.Language.Code.ToLower(),
                        StringComparison.Ordinal))
                        localizedCity = city;

                var localAddress = new LocalizedDtoAddress()
                {
                    Id = local.Id,
                    Country = localizedCountry.Name,
                    City = localizedCity.Name,
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

            var dtoCategory = new DtoDoctorCategoryMultiLang() { LocalizedCategory = new List<DtoDoctorCategoryLocalized>() };
            if (doctor.Category != null)
            {
                dtoCategory.Id = doctor.Category.Id;
                foreach (var local in doctor.Category.Localized)
                {
                    var localCategory = new DtoDoctorCategoryLocalized()
                    {
                        Id = local.Id,
                        Name = local.Name,
                        LangCode = local.Language?.Code?.ToLower()
                    };
                    dtoCategory.LocalizedCategory.Add(localCategory);
                }
            }

            var doctorLocals = new List<DtoDoctorLocalized>();
            foreach (var local in doctor.Localized)
            {
                var doctorLocal = new DtoDoctorLocalized()
                {
                    Id = local.Id,
                    Name = local.Name,
                    Manipulation = local.Manipulation,
                    Specialization = local.Specialization,
                    LangCode = local.Language?.Code.ToLower()
                };
                doctorLocals.Add(doctorLocal);
            }

            var clinicsIdList = doctor.Clinics.Select(clinic => clinic.Id).ToList();

            var dtoDoctor = new DtoDoctorMultiLang()
            {
                Id = doctor.Id,
                Email = doctor.Email,
                Experience = doctor.Experience,
                RatePoliteness = doctor.RatePoliteness,
                RateProfessionalism = doctor.RateProfessionalism,
                RateWaitingTime = doctor.RateWaitingTime,
                RateAverage = doctor.RateAverage,
                ReviewCount = doctor.DoctorReviews.Count,
                Image = pathToImage + doctor.ImageName,
                Favorite = doctor.Favorite,
                FavouriteExpireDate = doctor.FavouriteExpireDate,
                Recommended = doctor.Recommended,
                RecommendedExpireDate = doctor.RecommendedExpireDate,
                Address = dtoAddress,
                ClinicsIdList = clinicsIdList,
                Category = dtoCategory,
                LocalizedDoctor = doctorLocals
            };

            return dtoDoctor;
        }

        public void Add(DtoDoctorMultiLang newDoctorMultiLang)
        {
            if (newDoctorMultiLang == null)
                throw new ArgumentNullException(nameof(newDoctorMultiLang));

            throw new NotImplementedException();
            //var locals = new List<LocalizedDoctor>();

            //if (newDoctorMultiLang.Localized != null)
            //    foreach (var doctorLocalized in newDoctorMultiLang.Localized)
            //    {
            //        locals.Add(new LocalizedDoctor() { Name = doctorLocalized.Name, Manipulation = doctorLocalized.Manipulation, Language = _languageRepository.GetLanguageByCode(doctorLocalized.LangCode), Specialization = doctorLocalized.Specialization });
            //    }

            //var doctor = new Doctor()
            //{
            //    Localized = locals,
            //    Email = newDoctorMultiLang.Email,
            //    Experience = newDoctorMultiLang.Experience,
            //    ImageName = newDoctorMultiLang.Image
            //};

            //var clinicsList = new List<Clinic>();

            //foreach (var clinicId in newDoctorMultiLang.ClinicsIdList)
            //{
            //    var clinic = _clinicRepository.GetClinic(clinicId);
            //    if (clinic != null)
            //        clinicsList.Add(clinic);
            //}

            //doctor.Clinics = clinicsList;

            //var doctorCategoryList = _doctorCategoryRepository.GetAllCategories().ToList();

            //var category = new DoctorCategory();
            //if (newDoctorMultiLang.Category.Localized.Any())
            //{
            //    var local = newDoctorMultiLang.Category.Localized[0];
            //    category = doctorCategoryList.First(dc =>
            //    {
            //        var localizedDC = dc.Localized.FirstOrDefault(l => string.Equals(l.Language.Code, local.LangCode,
            //            StringComparison.CurrentCultureIgnoreCase));
            //        return localizedDC != null && string.Equals(localizedDC.Name, local.Name,
            //                   StringComparison.CurrentCultureIgnoreCase);
            //    });
            //}

            //doctor.Category = category;


            //_doctorRepository.Add(doctor);
            //_searchService.RefreshCache();
        }

        public void Update(int id, DtoDoctorMultiLang newDoctorMultiLang)
        {
            if (newDoctorMultiLang == null)
                throw new ArgumentNullException(nameof(newDoctorMultiLang));

            throw new NotImplementedException();

            //var locals = new List<LocalizedDoctor>();

            //if (newDoctorMultiLang.Localized != null)
            //    foreach (var doctorLocalized in newDoctorMultiLang.Localized)
            //    {
            //        locals.Add(new LocalizedDoctor() { Name = doctorLocalized.Name, Manipulation = doctorLocalized.Manipulation, Language = _languageRepository.GetLanguageByCode(doctorLocalized.LangCode), Specialization = doctorLocalized.Specialization });
            //    }

            //var doctor = _doctorRepository.GetDoctorById(id);

            //if (doctor == null)
            //    throw new ApplicationException($"Doctor with id={id} not found");

            //doctor.Localized = locals;
            //doctor.Email = newDoctorMultiLang.Email;
            //doctor.Experience = newDoctorMultiLang.Experience;
            //doctor.ImageName = newDoctorMultiLang.Image;

            //var doctorCategoryList = _doctorCategoryRepository.GetAllCategories().ToList();
            //var clinicsList = new List<Clinic>();

            //foreach (var clinicId in newDoctorMultiLang.ClinicsIdList)
            //{
            //    var clinic = _clinicRepository.GetClinic(clinicId);
            //    if (clinic != null)
            //        clinicsList.Add(clinic);
            //}

            //var category = new DoctorCategory();
            //if (newDoctorMultiLang.Category.Localized.Any())
            //{
            //    var local = newDoctorMultiLang.Category.Localized[0];
            //    category = doctorCategoryList.First(dc =>
            //    {
            //        var localizedDC = dc.Localized.FirstOrDefault(l => string.Equals(l.Language.Code, local.LangCode,
            //            StringComparison.CurrentCultureIgnoreCase));
            //        return localizedDC != null && string.Equals(localizedDC.Name, local.Name,
            //                   StringComparison.CurrentCultureIgnoreCase);
            //    });
            //}

            //doctor.Clinics = clinicsList;
            //doctor.Category = category;

            //_doctorRepository.Update(doctor);
            //_searchService.RefreshCache();
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

        private static DtoDoctorSingleLang ConvertEntityToDtoSingleLang(string lang, string pathToImage, Doctor doctor)
        {
            lang = lang.ToLower();

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

            var clinicsIdList = doctor.Clinics.Select(clinic => clinic.Id).ToList();

            var dtoDoctor = new DtoDoctorSingleLang()
            {
                Id = doctor.Id,
                LangCode = local.LangCode?.ToLower(),
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
                Image = pathToImage + doctor.ImageName,
                Favorite = doctor.Favorite,
                FavouriteExpireDate = doctor.FavouriteExpireDate,
                Recommended = doctor.Recommended,
                RecommendedExpireDate = doctor.RecommendedExpireDate,
                ClinicsIdList = clinicsIdList
            };

            if (doctor.Address != null)
            {
                var coords = new Coords()
                {
                    Lat = doctor.Address.Lat,
                    Lng = doctor.Address.Lng
                };

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
                        var localizedCountry = new LocalizedCountry();
                        foreach (var country in doctor.Address.Country.LocalizedCountries)
                            if (string.Equals(country.Language.Code.ToLower(), lang,
                                StringComparison.Ordinal))
                                localizedCountry = country;

                        var localizedCity = new LocalizedCity();
                        foreach (var city in doctor.Address.City.LocalizedCities)
                            if (string.Equals(city.Language.Code.ToLower(), lang,
                                StringComparison.Ordinal))
                                localizedCity = city;

                        localizedDtoAddress = new LocalizedDtoAddress()
                        {
                            Country = localizedCountry.Name,
                            City = localizedCity.Name,
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
                    Phones = dtoPhoneList,
                    Coords = coords
                };

                dtoDoctor.Address = dtoAddress;
            }

            if (doctor.Clinics == null)
                return dtoDoctor;

            dtoDoctor.ClinicsIdList = new List<int>();
            foreach (var clinic in doctor.Clinics)
            {
                dtoDoctor.ClinicsIdList.Add(clinic.Id);
            }
            return dtoDoctor;
        }
    }
}
