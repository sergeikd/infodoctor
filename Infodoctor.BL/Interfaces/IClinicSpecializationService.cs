using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IClinicSpecializationService
    {
        IEnumerable<DtoClinicSpecialization> GetAllSpecializations();
        DtoClinicSpecialization GetSpecializationById(int id);
        void Add(string name);
        void Update(int id,string name);
        void Delete(int id);
    }
}
