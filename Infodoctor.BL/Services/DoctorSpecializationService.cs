using System;
using System.Collections.Generic;
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
        private readonly IClinicSpecializationRepository _clinicSpecialization;
        private readonly ILanguageRepository _languageRepository;

        public DoctorSpecializationService(IDoctorSpecializationRepository doctorSpecializationRepository,
            ISearchService searchService, IClinicSpecializationRepository clinicSpecialization, ILanguageRepository languageRepository)
        {
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));
            if (clinicSpecialization == null) throw new ArgumentNullException(nameof(clinicSpecialization));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _searchService = searchService;
            _clinicSpecialization = clinicSpecialization;
            _languageRepository = languageRepository;
            _doctorSpecializationRepository = doctorSpecializationRepository;
        }

        public IEnumerable<DtoDoctorSpecializationSilngleLang> GetAllSpecializations(string lang)
        {
            var doctorSecializationList = _doctorSpecializationRepository.GetAllSpecializations();
            var dtoDoctorsSpecs = new List<DtoDoctorSpecializationSilngleLang>();

            foreach (var specialization in doctorSecializationList)
            {
                var local = new LocalizedDoctorSpecialization();
                if (specialization.LocalizedDoctorSpecializations != null)
                    foreach (var localizedSpecialization in specialization.LocalizedDoctorSpecializations)
                        if (string.Equals(localizedSpecialization.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase))
                            local = localizedSpecialization;

                var dtoSpecialized = new DtoDoctorSpecializationSilngleLang()
                {
                    Id = specialization.Id,
                    Name = local.Name,
                    LangCode = local.Language.Code
                };
                dtoDoctorsSpecs.Add(dtoSpecialized);
            }

            return dtoDoctorsSpecs;
        }

        public DtoDoctorSpecializationSilngleLang GetSpecializationById(int id, string lang)
        {
            var specialization = _doctorSpecializationRepository.GetSpecializationById(id);

            var local = new LocalizedDoctorSpecialization();
            if (specialization.LocalizedDoctorSpecializations != null)
                foreach (var localizedSpecialization in specialization.LocalizedDoctorSpecializations)
                    if (string.Equals(localizedSpecialization.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase))
                        local = localizedSpecialization;

            var dtoSpecialized = new DtoDoctorSpecializationSilngleLang()
            {
                Id = specialization.Id,
                Name = local.Name,
                LangCode = local.Language.Code
            };

            return dtoSpecialized;
        }

        public void Add(DtoDoctorSpecializationMultilagLang dtoDs)
        {
            if (dtoDs == null)
                throw new ArgumentNullException(nameof(dtoDs));

            var localizedNewDs = new List<LocalizedDoctorSpecialization>();
            if (dtoDs.Localized != null)
                foreach (var localized in dtoDs.Localized)
                {
                    var newLocal = new LocalizedDoctorSpecialization()
                    {
                        Id = localized.Id,
                        Name = localized.Name,
                        Language = _languageRepository.GetLanguageByCode(localized.LangCode)
                    };
                    localizedNewDs.Add(newLocal);
                }

            var newDs = new DoctorSpecialization()
            {
                Id = dtoDs.Id,
                ClinicSpecialization = _clinicSpecialization.GetClinicSpecializationById(dtoDs.Id),
                LocalizedDoctorSpecializations = localizedNewDs
            };

            _doctorSpecializationRepository.Add(newDs);
            _searchService.RefreshCache();
        }

        public void Update(DtoDoctorSpecializationMultilagLang dtoDs)
        {
            if (dtoDs == null)
                throw new ArgumentNullException(nameof(dtoDs));

            var doctorSpecialization = _doctorSpecializationRepository.GetSpecializationById(dtoDs.Id);
            if (doctorSpecialization != null)
            {
                var localizedNewDs = new List<LocalizedDoctorSpecialization>();
                if (dtoDs.Localized != null)
                    foreach (var localized in dtoDs.Localized)
                    {
                        var newLocal = new LocalizedDoctorSpecialization()
                        {
                            Id = localized.Id,
                            Name = localized.Name,
                            Language = _languageRepository.GetLanguageByCode(localized.LangCode)
                        };
                        localizedNewDs.Add(newLocal);
                    }

                doctorSpecialization.LocalizedDoctorSpecializations = localizedNewDs;
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
