using Infodoctor.Domain;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IСlinicRepository
    {
        IQueryable<Clinic> GetAllСlinics();
        Clinic GetClinicById(int id);
        void Add(Clinic clinic);
        void Update(Clinic clinic);
        void Delete(Clinic clinic);
    }
}
