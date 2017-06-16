using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ClinicSpecializationService : IClinicSpecializationService
    {
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ISearchService _searchService;

        public ClinicSpecializationService(IClinicSpecializationRepository clinicSpecializationRepository, ISearchService searchService, ILanguageRepository languageRepository)
        {
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _searchService = searchService;
            _languageRepository = languageRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        /// <param name="lang">2-character language code</param>
        public IEnumerable<DtoClinicSpecializationMultilang> GetAllSpecializations(string lang)
        {
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations();
            var dtoClinicSpecializationsList = new List<DtoClinicSpecializationMultilang>();
            foreach (var clinicSpecialization in clinicSpecializationList)
            {
                LocalizedDtoClinicSpecialization localizedDtoCs = null;

                foreach (var clinicSpecializationLocalizedClinicSpecialization in clinicSpecialization.LocalizedClinicSpecializations)
                {
                    if (clinicSpecializationLocalizedClinicSpecialization.Language.Code.ToLower() == lang.ToLower())
                        localizedDtoCs = new LocalizedDtoClinicSpecialization()
                        {
                            Id = clinicSpecializationLocalizedClinicSpecialization.Id,
                            Name = clinicSpecializationLocalizedClinicSpecialization.Name,
                            LangCode = clinicSpecializationLocalizedClinicSpecialization.Language.Code
                        };
                }

                dtoClinicSpecializationsList.Add(new DtoClinicSpecializationMultilang()
                {
                    Id = clinicSpecialization.Id,
                    LocalizedDtoClinicSpecializations = new List<LocalizedDtoClinicSpecialization>() { localizedDtoCs }
                });
            }

            return dtoClinicSpecializationsList;
        }

        /// <param name="id">SpecializationMultiLang id</param>
        /// <param name="lang">2-character language code</param>
        public DtoClinicSpecializationMultilang GetSpecializationById(int id, string lang)
        {
            var clinicSpecialization = _clinicSpecializationRepository.GetClinicSpecializationById(id);
            LocalizedDtoClinicSpecialization localizedDtoCs = null;

            foreach (var clinicSpecializationLocalizedClinicSpecialization in clinicSpecialization.LocalizedClinicSpecializations)
            {
                if (clinicSpecializationLocalizedClinicSpecialization.Language.Code.ToLower() == lang.ToLower())
                    localizedDtoCs = new LocalizedDtoClinicSpecialization()
                    {
                        Id = clinicSpecializationLocalizedClinicSpecialization.Id,
                        Name = clinicSpecializationLocalizedClinicSpecialization.Name,
                        LangCode = clinicSpecializationLocalizedClinicSpecialization.Language.Code
                    };
            }

            var dtoClinicSpecializations = new DtoClinicSpecializationMultilang()
            {
                Id = clinicSpecialization.Id,
                LocalizedDtoClinicSpecializations = new List<LocalizedDtoClinicSpecialization>() { localizedDtoCs }
            };

            return dtoClinicSpecializations;
        }

        /// <param name="multilangSpecialization">SpecializationMultiLang dto model</param>
        public void Add(DtoClinicSpecializationMultilang multilangSpecialization)
        {
            if (multilangSpecialization == null || !multilangSpecialization.LocalizedDtoClinicSpecializations.Any())
                throw new ArgumentNullException(nameof(multilangSpecialization));

            var localizations = new List<LocalizedClinicSpecialization>();
            foreach (var localizedDtoCs in multilangSpecialization.LocalizedDtoClinicSpecializations)
            {
                localizations.Add(new LocalizedClinicSpecialization()
                {
                    Name = localizedDtoCs.Name,
                    Language = _languageRepository.GetLanguageByCode(localizedDtoCs.LangCode)
                });
            }

            var cp = new ClinicSpecialization()
            {
                LocalizedClinicSpecializations = localizations
            };

            _clinicSpecializationRepository.Add(cp);
            _searchService.RefreshCache();
        }

        /// <param name="multilangSpecialization">SpecializationMultiLang dto model</param>
        public void Update(DtoClinicSpecializationMultilang multilangSpecialization)
        {
            if (multilangSpecialization == null || !multilangSpecialization.LocalizedDtoClinicSpecializations.Any())
                throw new ArgumentNullException(nameof(multilangSpecialization));

            var cp = _clinicSpecializationRepository.GetClinicSpecializationById(multilangSpecialization.Id);

            if (cp == null)
                throw new ArgumentNullException(nameof(cp));

            var localizations = new List<LocalizedClinicSpecialization>();
            foreach (var localizedDtoCs in multilangSpecialization.LocalizedDtoClinicSpecializations)
            {
                localizations.Add(new LocalizedClinicSpecialization()
                {
                    Name = localizedDtoCs.Name,
                    Language = _languageRepository.GetLanguageByCode(localizedDtoCs.LangCode)
                });
            }

            cp.LocalizedClinicSpecializations = localizations;

            _clinicSpecializationRepository.Update(cp);
            _searchService.RefreshCache();
        }

        public void Delete(int id)
        {
            var cp = _clinicSpecializationRepository.GetClinicSpecializationById(id);

            if (cp != null)
            {
                _clinicSpecializationRepository.Delete(cp);
                _searchService.RefreshCache();
            }
        }
    }
}
