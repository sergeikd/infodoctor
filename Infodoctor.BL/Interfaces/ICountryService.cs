using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<DtoCountry> GetAllCountries();
        DtoCountry GetCountryById(int id);
        void Add(DtoCountry country);
        void Update(DtoCountry country);
        void Delete(int id);
    }
}
