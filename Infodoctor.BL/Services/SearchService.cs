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
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;
        private readonly IDoctorSpecializationRepository _doctorSpecializationRepository;

        private static List<string> VirtualClinicsAndSpecialisationsCache { get; set; }
        private static List<string> VirtualDoctorsCache { get; set; }

        public SearchService(IСlinicRepository clinicRepository,
            IClinicSpecializationRepository clinicSpecializationRepository, 
            IDoctorRepository doctorRepository, 
            IDoctorSpecializationRepository doctorSpecializationRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));

            _doctorSpecializationRepository = doctorSpecializationRepository;
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        public void RefreshCache()
        {
            var clinics = _clinicRepository.GetAllСlinics();
            var clinicSpecializations = _clinicSpecializationRepository.GetAllClinicSpecializations();
            var doctors = _doctorRepository.GetAllDoctors();
            var doctorSpecializations = _doctorSpecializationRepository.GetAllSpecializations();

            var clinicsList = new List<string>();
            var specsList = new List<string>();
            var doctorsList = new List<string>();
            var doctorsSpecsList = new List<string>();

            foreach (var clinic in clinics)
                clinicsList.Add(clinic.Name);

            foreach (var cs in clinicSpecializations)
                specsList.Add(cs.Name);

            foreach (var doctor in doctors)
            {
                doctorsList.Add(doctor.Name);
                doctorsList.Add(doctor.Manipulation);
            }

            foreach (var ds in doctorSpecializations)
                doctorsSpecsList.Add(ds.Name);

            VirtualClinicsAndSpecialisationsCache = clinicsList;
            VirtualClinicsAndSpecialisationsCache.AddRange(specsList);

            VirtualDoctorsCache = doctorsList;
            VirtualDoctorsCache.AddRange(doctorsSpecsList);
        }

        private static bool IsVirtualCachesFull()
        {
            var flag = VirtualClinicsAndSpecialisationsCache != null || VirtualDoctorsCache != null;
            return flag;
        }

        public List<string> FastSearch(DtoFastSearchModel searchModel)
        {
            var result = new List<string>();

            if (IsVirtualCachesFull() == false)
                RefreshCache();
            foreach (var type in searchModel.TypeId)
                switch (type)
                {
                    case 1:
                        result.AddRange(VirtualClinicsAndSpecialisationsCache.Where(clinic => clinic.ToUpper().Contains(searchModel.Text.ToUpper())));
                        break;
                    case 2:
                        result.AddRange(VirtualDoctorsCache.Where(spec => spec.ToUpper().Contains(searchModel.Text.ToUpper())));
                        break;
                }
            return result;
        }
    }
}
