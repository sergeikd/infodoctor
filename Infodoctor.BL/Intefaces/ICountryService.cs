using System.Collections.Generic;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Intefaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAllCountries();
    }
}
