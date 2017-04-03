using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAllCountries();
        Country GetCountryById(int id);
        void Add(Country country);
        void Update(Country country);
        void Delete(Country country);
    }
}
