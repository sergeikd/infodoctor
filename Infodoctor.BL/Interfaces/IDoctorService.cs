using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorService
    {
        IEnumerable<DtoDoctor> GetAllDoctors(string pathToImage,string lang);
        DtoDoctor GetDoctorById(int id, string pathToImage, string lang);
        DtoPagedDoctor GetPagedDoctors(int perPage, int numPage, string pathToImage, string lang);
        DtoPagedDoctor SearchDoctors(int perPage, int numPage, DtoDoctorSearchModel searchModel, string pathToImage, string lang);
        void Add(DtoDoctor newDoctor);
        void Update(int id, DtoDoctor newDoctor);
        void Delete(int id);
    }
}
