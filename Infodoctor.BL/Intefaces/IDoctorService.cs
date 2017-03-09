using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface IDoctorService
    {
        IEnumerable<DtoDoctor> GetAllDoctors();
        DtoDoctor GetDoctorById(int id);
        DtoPagedDoctor GetPagedDoctors(int perPage, int numPage);
        void Add(DtoDoctor newDoctor);
        void Update(int id, DtoDoctor newDoctor);
        void Delete(int id);
    }
}
