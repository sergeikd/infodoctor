using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IDoctorSpecializationRepository
    {
        IQueryable<DoctorSpecializationMultiLang> GetAllSpecializations();
        DoctorSpecializationMultiLang GetSpecializationById(int id);
        void Add(DoctorSpecializationMultiLang specializationMultiLang);
        void Update(DoctorSpecializationMultiLang specializationMultiLang);
        void Delete(DoctorSpecializationMultiLang specializationMultiLang);
    }
}
