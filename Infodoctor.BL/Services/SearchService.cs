using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;

namespace Infodoctor.BL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IСlinicRepository _clinicRepository;
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;

        private static List<string> _virtualClinicsCache { get; set; }
        private static List<string> _virtualSpecssCache { get; set; }

        public SearchService(IСlinicRepository clinicRepository,
            IClinicSpecializationRepository clinicSpecializationRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            _clinicRepository = clinicRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        private void BuildVirtualCache()
        {
            var clinics = _clinicRepository.GetAllСlinics().ToList();
            var specs = _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();

            var clinicsList = new List<string>();
            var specsList = new List<string>();

            foreach (var clinic in clinics)
                clinicsList.Add(clinic.Name);
            foreach (var spec in specs)
                specsList.Add(spec.Name);

            _virtualClinicsCache = clinicsList;
            _virtualSpecssCache = specsList;
        }

        private bool IsVirtualCachesFull()
        {
            var flag = true;
            if (_virtualClinicsCache == null)
                flag = false;
            if (_virtualSpecssCache == null)
                flag = false;
            return flag;
        }

        public DtoSearchResultModel FullSeacrh(DtoSearchModel searchModel)
        {
            var clinicsSearchResurl = new List<DtoClinic>();
            var clinicSpecializations = new List<DtoClinicSpecialization>();

            if (searchModel.Text.Length > 2)
                foreach (var type in searchModel.TypeId)
                    switch (type)
                    {
                        case 1:
                            {
                                foreach (var city in searchModel.CityId)
                                    clinicsSearchResurl.AddRange(FullSearchClinics(city, searchModel.Text));
                                break;
                            }
                        case 2:
                            {
                                foreach (var city in searchModel.CityId)
                                    clinicSpecializations.AddRange(FullSearchClinicSpecializations(city, searchModel.Text));
                                break;
                            }
                    }

            var result = new DtoSearchResultModel
            {
                Clinics = clinicsSearchResurl,
                ClinicSpecializations = clinicSpecializations
            };
            return result;
        }

        public List<string> FastSearch(DtoSearchModel searchModel)
        {
            var result = new List<string>();

            if (IsVirtualCachesFull() == false)
                BuildVirtualCache();

            if (searchModel.Text.Length > 2)
                foreach (var type in searchModel.TypeId)
                    switch (type)
                    {
                        case 1:
                            result.AddRange(_virtualClinicsCache.Where(clinic => clinic.ToUpper().Contains(searchModel.Text.ToUpper())));
                            break;
                        case 2:
                            result.AddRange(_virtualSpecssCache.Where(spec => spec.ToUpper().Contains(searchModel.Text.ToUpper())));
                            break;

                            //case 1:
                            //    foreach (var city in searchModel.CityId)
                            //        result.AddRange(FastSearchClinics(city, searchModel.Text));
                            //    break;
                            //case 2:
                            //    foreach (var city in searchModel.CityId)
                            //        result.AddRange(FastSearchClinicSpecializations(city, searchModel.Text));
                            //    break;
                    }

            return result;
        }

        private IEnumerable<DtoClinic> FullSearchClinics(int cityId, string name)
        {
            var clinicsContext = _clinicRepository.GetAllСlinics();
            var clinics = clinicsContext.Where(c => c.Name.ToUpper().Contains(name.ToUpper())).ToList();


            if (cityId != -1)
            {
                var filtredClinics = (from clinic in clinics
                                      from adress in clinic.CityAddresses
                                      where adress.City.Id == cityId
                                      select clinic).ToList();
                clinics = filtredClinics;
            }


            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in clinics)
            {
                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Email = clinic.Email
                };
                var dtoClinicAddressList = new List<DtoAddress>();
                foreach (var clinicAddress in clinic.CityAddresses)
                {
                    var dtoClinicAddress = new DtoAddress
                    {
                        City = clinicAddress.City.Name,
                        Street = clinicAddress.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };
                    foreach (var clinicPhone in clinicAddress.ClinicPhones)
                    {
                        var dtoClinicPhone = new DtoPhone { Desc = clinicPhone.Description, Phone = clinicPhone.Number };
                        dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                    }
                    dtoClinicAddressList.Add(dtoClinicAddress);
                }
                dtoClinic.ClinicAddress = dtoClinicAddressList;

                var dtoSpecializationList =
                    clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
                dtoClinic.ClinicSpecialization = dtoSpecializationList;


                dtoClinicList.Add(dtoClinic);
            }

            return dtoClinicList;
        }

        private IEnumerable<string> FastSearchClinics(int cityId, string name)
        {
            var clinicsContext = _clinicRepository.GetAllСlinics();
            var clinics = clinicsContext.Where(c => c.Name.ToUpper().Contains(name.ToUpper())).ToList();
            var result = new List<string>();


            if (cityId != -1)
            {
                var filtredClinics = (from clinic in clinics
                                      from adress in clinic.CityAddresses
                                      where adress.City.Id == cityId
                                      select clinic).ToList();
                clinics = filtredClinics;
            }

            foreach (var clinic in clinics)
                result.Add(clinic.Name);

            return result;
        }

        private IEnumerable<DtoClinicSpecialization> FullSearchClinicSpecializations(int cityId, string name)
        {
            var specs = _clinicSpecializationRepository.GetAllClinicSpecializations();
            var clinicSpecializations =
                specs.Where(s => s.Name.ToUpper().Contains(name.ToUpper())).ToList();

            if (cityId != -1)
            {
                var filtredClinicSpecializations = (from cs in clinicSpecializations
                                                    from clinic in cs.Clinics
                                                    from adress in clinic.CityAddresses
                                                    where adress.City.Id == cityId
                                                    select cs).ToList();
                clinicSpecializations = filtredClinicSpecializations;
            }

            var dtoClinicSpecializations = new List<DtoClinicSpecialization>();

            foreach (var cs in clinicSpecializations)
            {
                var dtoClinicList = new List<DtoClinic>();
                foreach (var clinic in cs.Clinics)
                {
                    var dtoClinic = new DtoClinic
                    {
                        Id = clinic.Id,
                        Name = clinic.Name,
                        Email = clinic.Email
                    };
                    var dtoClinicAddressList = new List<DtoAddress>();
                    foreach (var clinicAddress in clinic.CityAddresses)
                    {
                        var dtoClinicAddress = new DtoAddress
                        {
                            City = clinicAddress.City.Name,
                            Street = clinicAddress.Street,
                            ClinicPhones = new List<DtoPhone>()
                        };
                        foreach (var clinicPhone in clinicAddress.ClinicPhones)
                        {
                            var dtoClinicPhone = new DtoPhone
                            {
                                Desc = clinicPhone.Description,
                                Phone = clinicPhone.Number
                            };
                            dtoClinicAddress.ClinicPhones.Add(dtoClinicPhone);
                        }
                        dtoClinicAddressList.Add(dtoClinicAddress);
                    }
                    dtoClinic.ClinicAddress = dtoClinicAddressList;

                    var dtoSpecializationList =
                        clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
                    dtoClinic.ClinicSpecialization = dtoSpecializationList;


                    dtoClinicList.Add(dtoClinic);
                }

                var dtoCs = new DtoClinicSpecialization
                {
                    Id = cs.Id,
                    Name = cs.Name,
                    Clinic = dtoClinicList
                };
                dtoClinicSpecializations.Add(dtoCs);
            }

            return dtoClinicSpecializations;
        }

        private IEnumerable<string> FastSearchClinicSpecializations(int cityId, string name)
        {
            var specs = _clinicSpecializationRepository.GetAllClinicSpecializations();
            var clinicSpecializations =
                specs.Where(s => s.Name.ToUpper().Contains(name.ToUpper())).ToList();
            var result = new List<string>();

            if (cityId != -1)
            {
                var filtredClinicSpecializations = (from cs in clinicSpecializations
                                                    from clinic in cs.Clinics
                                                    from adress in clinic.CityAddresses
                                                    where adress.City.Id == cityId
                                                    select cs).ToList();
                clinicSpecializations = filtredClinicSpecializations;
            }

            foreach (var cs in clinicSpecializations)
                result.Add(cs.Name);

            return result;
        }
    }
}
