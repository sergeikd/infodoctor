using Infodoctor.Domain;
using System;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicProfilesRepository
    {
        IQueryable<ClinicProfile> GetAllClinicProfiles();
        ClinicProfile GetClinicProfileById(int id);
        void Add(ClinicProfile clinicProfile);
        void Update(ClinicProfile clinicProfile);
        void Delete(ClinicProfile clinicProfile);
    }
}
