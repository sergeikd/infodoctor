using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<DtoCountrySingleLang> GetAllCountries(string lang);
        DtoCountrySingleLang GetCountryById(int id, string lang);
        void Add(DtoCountryMultiLang country);
        void Update(DtoCountryMultiLang country);
        void Delete(int id);
    }
}
