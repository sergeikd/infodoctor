using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces.AddressInterfaces
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAllCountries();
        Country GetCountryById(int id);
        void Add(Country region);
        void Update(Country region);
        void Delete(Country region);
    }
}
