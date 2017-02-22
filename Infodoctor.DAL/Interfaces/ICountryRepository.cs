using System.Linq;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Interfaces
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAllCountries();
    }
}
