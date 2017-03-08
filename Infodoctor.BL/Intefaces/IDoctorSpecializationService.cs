using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Intefaces
{
    public interface IDoctorSpecializationService
    {
        IEnumerable<DtoDoctorSpecialisation> GetAllSpecializations();
        DtoDoctorSpecialisation GetSpecializationById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
