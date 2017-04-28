using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
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

        public IEnumerable<DtoDoctorSpecialization> GetAllSpecializations()
        {
            var doctorSecializationList = _doctorSpecializationRepository.GetAllSpecializations();

            return doctorSecializationList.Select(doctorSecialization => new DtoDoctorSpecialization()
            {
                Id = doctorSecialization.Id, Name = doctorSecialization.Name //,
                //ClinicSpecializationId = doctorSecialization.ClinicSpecialization.Id,
                //Doctors = new List<int>()
            }).ToList();
        }

        public DtoDoctorSpecialization GetSpecializationById(int id)
        {
            var doctorSpecialization = _doctorSpecializationRepository.GetSpecializationById(id);

            var dtoDs = new DtoDoctorSpecialization()
            {
                Id = doctorSpecialization.Id,
                Name = doctorSpecialization.Name,
                //ClinicSpecializationId = doctorSpecialization.ClinicSpecialization.Id,
                //Doctors = new List<int>()
            };

            //foreach (var doctor in doctorSpecialization.Doctors)
            //{
            //    dtoDs.Doctors.Add(doctor.Id);
            //}

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

            var doctorSpecialization = _doctorSpecializationRepository.GetSpecializationById(id);
            if (doctorSpecialization != null)
            {
                doctorSpecialization.Name = name;
                _doctorSpecializationRepository.Update(doctorSpecialization);
                _searchService.RefreshCache();
            }
        }

        public void Delete(int id)
        {
            var doctorSpecialization = _doctorSpecializationRepository.GetSpecializationById(id);
            if (doctorSpecialization == null) return;
            _doctorSpecializationRepository.Delete(doctorSpecialization);
            _searchService.RefreshCache();
        }
    }
}
