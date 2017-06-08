using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface ICitiesService
    {
        IEnumerable<DtoCity> GetAllCities(string lang);
        IEnumerable<DtoCity> GetCitiesWithClinics(string lang);
        DtoCity GetCityById(int id, string lang);
        void Add(string name, string lang);
        void Update(int id, string name, string lang);
        void Delete(int id);
    }
}
