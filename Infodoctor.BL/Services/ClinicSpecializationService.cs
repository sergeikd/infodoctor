using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.DAL.Repositories;

namespace Infodoctor.BL.Services
{
    public class ClinicSpecializationService : IClinicSpecializationService
    {
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;

        public ClinicSpecializationService(IClinicSpecializationRepository clinicSpecializationRepository)
        {
            if (clinicSpecializationRepository == null)
            {
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            }
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        public IEnumerable<ClinicSpecialization> GetAllProfiles()
        {
            return _clinicSpecializationRepository.GetAllClinicProfiles().ToList();
        }

        public ClinicSpecialization GetProfileById(int id)
        {
            return _clinicSpecializationRepository.GetClinicProfileById(id);
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var cp = new ClinicSpecialization() { Name = name };

            _clinicSpecializationRepository.Add(cp);
        }


        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var cp = _clinicSpecializationRepository.GetClinicProfileById(id);
            if (cp != null)
            {
                cp.Name = name;

                _clinicSpecializationRepository.Update(cp);
            }

        }

        public void Delete(int id)
        {
            var cp = _clinicSpecializationRepository.GetClinicProfileById(id);

            if (cp != null)
                _clinicSpecializationRepository.Delete(cp);
        }
    }
}
