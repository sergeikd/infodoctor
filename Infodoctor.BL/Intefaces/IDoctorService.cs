using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface IDoctorService
    {
        IEnumerable<DtoDoctor> GetAllDoctors(string pathToImage);
        DtoDoctor GetDoctorById(int id, string pathToImage);
        DtoPagedDoctor GetPagedDoctors(int perPage, int numPage, string pathToImage);
        void Add(DtoDoctor newDoctor);
        void Update(int id, DtoDoctor newDoctor);
        void Delete(int id);
    }
}
