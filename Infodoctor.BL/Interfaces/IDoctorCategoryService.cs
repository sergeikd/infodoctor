using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorCategoryService
    {
        IEnumerable<DtoDoctorCategorySingleLang> GetAllCategories(string lang);
        DtoDoctorCategorySingleLang GetCategoryById(int id, string lang);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
