using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IResortTypeService
    {
        IEnumerable<DtoResortTypeSingleLang> GetTypes(string lang);
        DtoResortTypeSingleLang GetType(int id, string lang);
        DtoResortTypeMultiLang GetType(int id);
        void Add(DtoResortTypeMultiLang type);
        void Update(DtoResortTypeMultiLang type);
        void Delete(int id);
    }
}
