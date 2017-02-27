using Infodoctor.Domain;
using System;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicSpecializationRepository
    {
        IQueryable<ClinicSpecialization> GetAllClinicProfiles();
        ClinicSpecialization GetClinicProfileById(int id);
        void Add(ClinicSpecialization clinicProfile);
        void Update(ClinicSpecialization clinicProfile);
        void Delete(ClinicSpecialization clinicProfile);
    }
}
