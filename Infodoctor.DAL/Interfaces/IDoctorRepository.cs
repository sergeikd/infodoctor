using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IDoctorRepository
    {
        IQueryable<Doctor> GetAllDoctors();
        IQueryable<Doctor> GetSortedDoctors(string sortBy, bool descending,string lang);
        Doctor GetDoctorById(int id);
        void Add(Doctor doctor);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
    }
}
