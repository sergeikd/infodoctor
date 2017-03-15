using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ClinicSpecialization> GetAllSpecializations()
        {
            return _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();
        }

        public ClinicSpecialization GetSpecializationById(int id)
        {
            return _clinicSpecializationRepository.GetClinicSpecializationById(id);
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
