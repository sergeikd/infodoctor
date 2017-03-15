using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class DoctorSpecializationService : IDoctorSpecializationService
    {
        private readonly IDoctorSpecializationRepository _doctorSpecializationRepository;
        private readonly ISearchService _searchService;

        public DoctorSpecializationService(IDoctorSpecializationRepository doctorSpecializationRepository,
            IClinicSpecializationService clinicSpecializationService,
            ISearchService searchService)
        {
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));

            _searchService = searchService;
            _doctorSpecializationRepository = doctorSpecializationRepository;
        }

        public IEnumerable<DtoDoctorSpecialisation> GetAllSpecializations()
        {
            var dsList = _doctorSpecializationRepository.GetAllSpecializations().ToList();
            var dtoDsList = new List<DtoDoctorSpecialisation>();

            foreach (var ds in dsList)
            {
                var dtoDs = new DtoDoctorSpecialisation()
                {
                    Id = ds.Id,
                    Name = ds.Name,
                    ClinicSpecializationId = ds.ClinicSpecialization.Id,
                    Doctors = new List<int>()
                };

                foreach (var doctor in ds.Doctors)
                {
                    dtoDs.Doctors.Add(doctor.Id);
                }

                dtoDsList.Add(dtoDs);
            }

            return dtoDsList;
        }

        public DtoDoctorSpecialisation GetSpecializationById(int id)
        {
            var ds = _doctorSpecializationRepository.GetSpecializationById(id);

            var dtoDs = new DtoDoctorSpecialisation()
            {
                Id = ds.Id,
                Name = ds.Name,
                ClinicSpecializationId = ds.ClinicSpecialization.Id,
                Doctors = new List<int>()
            };

            foreach (var doctor in ds.Doctors)
            {
                dtoDs.Doctors.Add(doctor.Id);
            }

            return dtoDs;
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            _doctorSpecializationRepository.Add(
                new DoctorSpecialization() { Name = name });
            _searchService.RefreshCache();
        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var ds = _doctorSpecializationRepository.GetSpecializationById(id);
            if (ds != null)
            {
                ds.Name = name;
                _doctorSpecializationRepository.Update(ds);
                _searchService.RefreshCache();
            }
        }

        public void Delete(int id)
        {
            var ds = _doctorSpecializationRepository.GetSpecializationById(id);
            if (ds != null)
            {
                _doctorSpecializationRepository.Delete(ds);
                _searchService.RefreshCache();
            }    
        }
    }
}
