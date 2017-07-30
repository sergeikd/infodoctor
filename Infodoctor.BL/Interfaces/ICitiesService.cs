using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface ICitiesService
    {
        IEnumerable<DtoCitySingleLang> GetAllCities(string lang);
        IEnumerable<DtoCitySingleLang> GetCitiesWithClinics(string lang);
        DtoCitySingleLang GetCity(int id, string lang);
        DtoCitySingleLang GetCity(string name, string lang);
        void Add(DtoCityMultiLang city);
        void Update(DtoCityMultiLang city);
        void Delete(int id);
    }
}
