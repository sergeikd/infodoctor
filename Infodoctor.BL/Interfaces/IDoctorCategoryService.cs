using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorCategoryService
    {
        IEnumerable<DtoDoctorCategory> GetAllCategories();
        DtoDoctorCategory GetCategoryById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
