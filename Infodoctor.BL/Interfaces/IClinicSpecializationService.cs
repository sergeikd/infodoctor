using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IClinicSpecializationService
    {
        IEnumerable<DtoClinicSpecialization> GetAllSpecializations(string lang);
        DtoClinicSpecialization GetSpecializationById(int id, string lang);
        void Add(DtoClinicSpecialization specialization);
        void Update(DtoClinicSpecialization specialization);
        void Delete(int id);
    }
}
