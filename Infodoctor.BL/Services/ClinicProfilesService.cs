using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infodoctor.BL.Services
{
    public class ClinicProfilesService : IClinicProfilesService
    {
        private readonly IClinicProfilesRepository _clinicProfilesRepository;

        public ClinicProfilesService(IClinicProfilesRepository clinicProfilesRepository)
        {
            if (clinicProfilesRepository == null)
            {
                throw new ArgumentNullException(nameof(clinicProfilesRepository));
            }
            _clinicProfilesRepository = clinicProfilesRepository;
        }

        public IEnumerable<ClinicProfile> GetAllProfiles()
        {
            return _clinicProfilesRepository.GetAllClinicProfiles().ToList();
        }

        public ClinicProfile GetProfileById(int id)
        {
            return _clinicProfilesRepository.GetClinicProfileById(id);
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var cp = new ClinicProfile() { Name = name };

            _clinicProfilesRepository.Add(cp);
        }


        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var cp = _clinicProfilesRepository.GetClinicProfileById(id);
            if (cp != null)
            {
                cp.Name = name;

                _clinicProfilesRepository.Update(cp);
            }

        }

        public void Delete(int id)
        {
            var cp = _clinicProfilesRepository.GetClinicProfileById(id);

            if (cp != null)
                _clinicProfilesRepository.Delete(cp);
        }
    }
}
