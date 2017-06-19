using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;

namespace Infodoctor.BL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;
        private readonly IDoctorSpecializationRepository _doctorSpecializationRepository;
        private readonly IResortRepository _resortRepository;
        private readonly ILanguageRepository _languageRepository;

        private static List<CacheModel> VirtualClinicsAndSpecialisationsCache { get; set; }
        private static List<CacheModel> VirtualDoctorsCache { get; set; }
        private static List<CacheModel> VirtualResortCache { get; set; }

        public SearchService(IClinicRepository clinicRepository,
            IClinicSpecializationRepository clinicSpecializationRepository,
            IDoctorRepository doctorRepository,
            IDoctorSpecializationRepository doctorSpecializationRepository,
            IResortRepository resortRepository,
            ILanguageRepository languageRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));
            if (resortRepository == null) throw new ArgumentNullException(nameof(resortRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _doctorSpecializationRepository = doctorSpecializationRepository;
            _resortRepository = resortRepository;
            _languageRepository = languageRepository;
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        public void RefreshCache()
        {
            var langs = _languageRepository.GetLanguages().ToList();
            var clinics = _clinicRepository.GetAllСlinics().ToList();
            var doctors = _doctorRepository.GetAllDoctors().ToList();

            var clinicsAndSpecsCaches = new List<CacheModel>();
            var doctorsCaches = new List<CacheModel>();

            foreach (var lang in langs)
            {
                var clinicCache = new CacheModel() { Lang = lang.Code, Words = new List<string>() };
                var doctorsCache = new CacheModel() { Lang = lang.Code, Words = new List<string>() };

                foreach (var clinic in clinics)
                {
                    var local = clinic.Localized.FirstOrDefault(l => string.Equals(l.Language.Code, lang.Code,
                         StringComparison.CurrentCultureIgnoreCase));
                    if (local != null)
                        clinicCache.Words.Add(local.Name);
                }

                foreach (var clinic in clinics)
                {
                    foreach (var cs in clinic.ClinicSpecializations)
                    {
                        var local =
                            cs.LocalizedClinicSpecializations.FirstOrDefault(
                                l => string.Equals(l.Language.Code, lang.Code, StringComparison.CurrentCultureIgnoreCase));
                        if (local == null)
                            continue;

                        clinicCache.Words.Add(local.Name);
                    }
                }

                foreach (var doctor in doctors)
                {
                    var local = doctor.Localized.FirstOrDefault(l => string.Equals(l.Language.Code, lang.Code,
                        StringComparison.CurrentCultureIgnoreCase));

                    if (local == null)
                        continue;

                    doctorsCache.Words.Add(local.Name);
                    doctorsCache.Words.Add(local.Manipulation);
                }

                foreach (var doctor in doctors)
                {
                    var local = doctor.Specialization.LocalizedDoctorSpecializations.FirstOrDefault(l => string.Equals(
                        l.Language.Code, lang.Code, StringComparison.CurrentCultureIgnoreCase));

                    if (local == null)
                        continue;

                    doctorsCache.Words.Add(local.Name);
                }

                clinicsAndSpecsCaches.Add(clinicCache);
                doctorsCaches.Add(doctorsCache);
            }

            VirtualClinicsAndSpecialisationsCache = clinicsAndSpecsCaches;
            VirtualDoctorsCache = doctorsCaches;
        }

        private static bool IsVirtualCachesFull()
        {
            var flag = VirtualClinicsAndSpecialisationsCache != null && VirtualDoctorsCache != null;
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
                        result.AddRange(VirtualClinicsAndSpecialisationsCache.FirstOrDefault(l => l.Lang.ToLower() == searchModel.Lang.ToLower()).Words.Where(word => word.ToLower().Contains(searchModel.Text.ToLower())));
                        break;
                    case 2:
                        result.AddRange(VirtualDoctorsCache.FirstOrDefault(l => l.Lang.ToLower() == searchModel.Lang.ToLower()).Words.Where(word => word.ToLower().Contains(searchModel.Text.ToLower())));
                        break;
                        //case 3:
                        //    result.AddRange(VirtualResortCache.Where(res => res.ToUpper().Contains(searchModel.Text.ToUpper())));
                        //    break;
                }
            return result;

        }
    }

    internal class CacheModel
    {
        public string Lang { get; set; }
        public List<string> Words { get; set; }
    }
}
