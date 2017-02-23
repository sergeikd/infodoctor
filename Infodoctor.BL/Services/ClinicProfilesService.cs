using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
