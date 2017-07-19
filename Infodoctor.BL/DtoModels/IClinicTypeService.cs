using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public interface IClinicTypeService
    {
        IEnumerable<DtoClinicTypeSingleLang> GetTypes(string lang);
        DtoClinicTypeSingleLang GetType(int id, string lang);
        DtoClinicTypeMultiLang GetType(int id);
        void Add(DtoClinicTypeMultiLang type);
        void Update(DtoClinicTypeMultiLang type);
        void Delete(int id);
    }
}
