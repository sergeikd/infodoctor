using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IDoctorCategoryRepository
    {
        IQueryable<DoctorCategory> GetAllCategories();
        DoctorCategory GetCategoryById(int id);
        void Add(DoctorCategory category);
        void Update(DoctorCategory category);
        void Delete(DoctorCategory category);
    }
}
