using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAllCountries();
    }
}
