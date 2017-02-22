using System.Collections.Generic;
using Infodoctor.Domain;

namespace Infodoctor.BL.Intefaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAllCountries();
    }
}
