using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces.AddressInterfaces
{
    public interface ICountryService
    {
        IEnumerable<DtoCountrySingleLang> GetAllCountries(string lang);
        IEnumerable<DtoCountryMultiLang> GetAllCountriesForAdmin();
        DtoCountrySingleLang GetCountryById(int id, string lang);
        DtoCountryMultiLang GetCountryByIdForAdmin(int id);
        void Add(DtoCountryMultiLang country);
        void Update(DtoCountryMultiLang country);
        void Delete(int id);
    }
}
