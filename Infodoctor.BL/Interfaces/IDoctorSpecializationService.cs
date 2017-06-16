using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorSpecializationService
    {
        IEnumerable<DtoDoctorSpecializationSilngleLang> GetAllSpecializations(string lang);
        DtoDoctorSpecializationSilngleLang GetSpecializationById(int id, string lang);
        void Add(DtoDoctorSpecializationMultilagLang dtoDs);
        void Update(DtoDoctorSpecializationMultilagLang dtoDs);
        void Delete(int id);
    }
}
