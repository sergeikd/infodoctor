using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ClinicSpecializationService : IClinicSpecializationService
    {
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;
        private readonly ISearchService _searchService;

        public ClinicSpecializationService(IClinicSpecializationRepository clinicSpecializationRepository, ISearchService searchService)
        {
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));

            _searchService = searchService;
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        public IEnumerable<DtoClinicSpecialization> GetAllSpecializations()
        {
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations();
            return clinicSpecializationList.Select(clinicSpecialization => new DtoClinicSpecialization() {Id = clinicSpecialization.Id, Name = clinicSpecialization.Name}).ToList();
        }

        public DtoClinicSpecialization GetSpecializationById(int id)
        {
            var clinicSpecialization = _clinicSpecializationRepository.GetClinicSpecializationById(id);
            return new DtoClinicSpecialization() {Id = clinicSpecialization.Id, Name = clinicSpecialization.Name};
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var cp = new ClinicSpecialization() { Name = name };

            _clinicSpecializationRepository.Add(cp);
            _searchService.RefreshCache();
        }


        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var cp = _clinicSpecializationRepository.GetClinicSpecializationById(id);
            if (cp != null)
            {
                cp.Name = name;

                _clinicSpecializationRepository.Update(cp);
                _searchService.RefreshCache();
            }

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
