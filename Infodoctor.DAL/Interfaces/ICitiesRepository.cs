using Infodoctor.Domain;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface ICitiesRepository
    {
        IQueryable<City> GetAllCities();
        IQueryable<City> GetAllCitiesWithClinics();
        City GetCityById(int id);
        void Add(City city);
        void Update(City city);
        void Delete(City city);
    }
}
