using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorService
    {
        IEnumerable<DtoDoctorSingleLang> GetAllDoctors(string pathToImage, string lang);
        DtoDoctorSingleLang GetDoctorById(int id, string pathToImage, string lang);
        DtoDoctorMultiLang GetDoctorById(int id, string pathToImage);
        DtoPagedDoctor GetPagedDoctors(int perPage, int numPage, string pathToImage, string lang);
        DtoPagedDoctor SearchDoctors(int perPage, int numPage, DtoDoctorSearchModel searchModel, string pathToImage, string lang);
        void Add(DtoDoctorMultiLang newDoctorMultiLang);
        void Update(int id, DtoDoctorMultiLang newDoctorMultiLang);
        void Delete(int id);
    }
}
