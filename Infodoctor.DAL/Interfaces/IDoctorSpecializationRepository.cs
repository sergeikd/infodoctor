using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IDoctorSpecializationRepository
    {
        IQueryable<DoctorSpecialization> GetAllSpecializations();
        DoctorSpecialization GetSpecializationById(int id);
        void Add(DoctorSpecialization specialization);
        void Update(DoctorSpecialization specialization);
        void Delete(DoctorSpecialization specialization);
    }
}
