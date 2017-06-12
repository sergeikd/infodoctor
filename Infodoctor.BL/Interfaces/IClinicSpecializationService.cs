using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IClinicSpecializationService
    {
        IEnumerable<DtoClinicSpecializationMultilang> GetAllSpecializations(string lang);
        DtoClinicSpecializationMultilang GetSpecializationById(int id, string lang);
        void Add(DtoClinicSpecializationMultilang multilangSpecialization);
        void Update(DtoClinicSpecializationMultilang multilangSpecialization);
        void Delete(int id);
    }
}
