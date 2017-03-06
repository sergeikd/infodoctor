using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicSpecializationRepository
    {
        IQueryable<ClinicSpecialization> GetAllClinicSpecializations();
        ClinicSpecialization GetClinicSpecializationById(int id);
        void Add(ClinicSpecialization clinicSpecialization);
        void Update(ClinicSpecialization clinicSpecialization);
        void Delete(ClinicSpecialization clinicSpecialization);
    }
}
