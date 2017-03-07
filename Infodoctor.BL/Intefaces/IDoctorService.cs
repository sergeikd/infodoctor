using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface IDoctorService
    {
        IEnumerable<DtoDoctor> GetAllDoctors();
        DtoDoctor GetDoctorById(int id);
        void Add(DtoDoctor newDoctor);
        void Update(int id, DtoDoctor newDoctor);
        void Delete(int id);
    }
}
