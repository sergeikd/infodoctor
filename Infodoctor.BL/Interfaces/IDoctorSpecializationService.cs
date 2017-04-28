using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorSpecializationService
    {
        IEnumerable<DtoDoctorSpecialization> GetAllSpecializations();
        DtoDoctorSpecialization GetSpecializationById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
