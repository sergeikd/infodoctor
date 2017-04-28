using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface ICitiesService
    {
        IEnumerable<DtoCity> GetAllCities();
        IEnumerable<DtoCity> GetCitiesWithClinics();
        DtoCity GetCityById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
