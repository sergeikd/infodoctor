using System.Collections.Generic;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicRepository
    {
        IQueryable<Clinic> GetAllСlinics();
        IQueryable<Clinic> GetSortedСlinics(string sortBy, bool descending, string lang);
        Clinic GetClinicById(int id);
        void Add(Clinic clinic);
        void AddMany(IEnumerable<Clinic> clinics);
        void Update(Clinic clinic);
        void Delete(Clinic clinic);
    }

    public interface ILocalizedClinicRepository
    {
        IQueryable<LocalizedClinic> GetAllLocalizedClinics();
        LocalizedClinic GetLocalizedClinicById(int id);
        void Add(LocalizedClinic clinic);
        void Update(LocalizedClinic clinic);
        void Delete(LocalizedClinic clinic);
    }
}
