using System.Collections.Generic;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Intefaces
{
    public interface IDoctorSpecializationService
    {
        IEnumerable<DoctorSpecialization> GetAllSpecializations();
        DoctorSpecialization GetSpecializationById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
