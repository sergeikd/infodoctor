using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IСlinicRepository
    {
        IQueryable<Clinic> GetAllСlinics();
        IQueryable<Clinic> GetSortedСlinics(string sortBy, bool descending);
        Clinic GetClinicById(int id);
        void Add(Clinic clinic);
        void Update(Clinic clinic);
        void Delete(Clinic clinic);
    }
}
