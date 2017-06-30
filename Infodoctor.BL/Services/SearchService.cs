using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IResortRepository _resortRepository;
        private readonly ILanguageRepository _languageRepository;

        private static List<CacheModel> VirtualClinicsAndSpecialisationsCache { get; set; }
        private static List<CacheModel> VirtualDoctorsCache { get; set; }
        private static List<CacheModel> VirtualResortCache { get; set; }

        public SearchService(IClinicRepository clinicRepository,
            IDoctorRepository doctorRepository,
            IResortRepository resortRepository,
            ILanguageRepository languageRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (resortRepository == null) throw new ArgumentNullException(nameof(resortRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _resortRepository = resortRepository;
            _languageRepository = languageRepository;
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
        }

        public void RefreshCache()
        {
            var langs = _languageRepository.GetLanguages().ToList();
            var clinics = _clinicRepository.GetAllСlinics().ToList();
            var doctors = _doctorRepository.GetAllDoctors().ToList();
            var resorts = _resortRepository.GetAllResorts().ToList();

            var clinicsAndSpecsCaches = new List<CacheModel>();
            var doctorsCaches = new List<CacheModel>();
            var resortsCaches = new List<CacheModel>();

            foreach (var lang in langs)
            {
                var clinicCache = new CacheModel() { Lang = lang.Code, Words = new List<string>() };
                var doctorsCache = new CacheModel() { Lang = lang.Code, Words = new List<string>() };
                var resortsCache = new CacheModel() { Lang = lang.Code, Words = new List<string>() };

                //for (var i = 0; i < 1370; i++) для теста. Создаст в районе 150к записей из врачей
                foreach (var clinic in clinics)
                {
                    LocalizedClinic local;
                    try
                    {
                        local = clinic.Localized.First(l => string.Equals(l.Language.Code, lang.Code,
                            StringComparison.CurrentCultureIgnoreCase));
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (local == null)
                        continue;

                    clinicCache.Words.Add(local.Name.ToLower());

                    if (local.Specializations == null) continue;
                    var specs = local.Specializations.Split('|');
                    var lowerSpecs = new List<string>();
                    foreach (var spec in specs)
                        lowerSpecs.Add(spec.ToLower());

                    clinicCache.Words.AddRange(lowerSpecs);
                }

                foreach (var doctor in doctors)
                {
                    LocalizedDoctor local;
                    try
                    {
                        local = doctor.Localized.First(l => string.Equals(l.Language.Code, lang.Code,
                            StringComparison.CurrentCultureIgnoreCase));
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (local == null)
                        continue;

                    doctorsCache.Words.Add(local.Name.ToLower());
                    doctorsCache.Words.Add(local.Manipulation.ToLower());

                    if (local.Specialization == null) continue;
                    var specs = local.Specialization.Split('|');
                    var lowerSpecs = new List<string>();
                    foreach (var spec in specs)
                        lowerSpecs.Add(spec.ToLower());

                    doctorsCache.Words.AddRange(lowerSpecs);
                }

                foreach (var resort in resorts)
                {
                    LocalizedResort local;
                    try
                    {
                        local = resort.Localized.First(l => string.Equals(l.Language.Code, lang.Code,
                            StringComparison.CurrentCultureIgnoreCase));
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (local == null) continue;

                    resortsCache.Words.Add(local.Name.ToLower());

                    if (string.IsNullOrEmpty(local.Manipulations)) continue;

                    resortsCache.Words.Add(local.Manipulations.ToLower());
                }

                clinicsAndSpecsCaches.Add(clinicCache);
                doctorsCaches.Add(doctorsCache);
                resortsCaches.Add(resortsCache);
            }

            VirtualClinicsAndSpecialisationsCache = clinicsAndSpecsCaches;
            VirtualDoctorsCache = doctorsCaches;
            VirtualResortCache = resortsCaches;
        }

        private static bool IsVirtualCachesFull()
        {
            var flag = VirtualClinicsAndSpecialisationsCache != null && VirtualDoctorsCache != null;
            return flag;
        }


        public List<string> FastSearch(DtoFastSearchModel searchModel)
        {
            searchModel.LangCode = searchModel.LangCode.ToLower();
            searchModel.Text = searchModel.Text.ToLower();

            var result = new List<string>();

            if (IsVirtualCachesFull() == false)
                RefreshCache();
            foreach (var type in searchModel.TypeId)
                switch (type)
                {
                    case 1:
                        var clinicCache =
                            VirtualClinicsAndSpecialisationsCache.FirstOrDefault(l => l.Lang == searchModel.LangCode);
                        if (clinicCache != null)
                            result.AddRange(clinicCache.Words.Where(word => word.Contains(searchModel.Text)));
                        break;
                    case 2:
                        var doctorCache = VirtualDoctorsCache.FirstOrDefault(l => l.Lang == searchModel.LangCode);
                        if (doctorCache != null)
                            result.AddRange(doctorCache.Words.Where(word => word.Contains(searchModel.Text)));
                        break;
                    case 3:
                        var resortCache = VirtualResortCache.FirstOrDefault(l => l.Lang == searchModel.LangCode);
                        if (resortCache != null)
                            result.AddRange(resortCache.Words.Where(word => word.Contains(searchModel.Text)));
                        break;
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
