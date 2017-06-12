using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IClinicService
    {
        IEnumerable<DtoClinicSingleLang> GetAllClinics(string pathToImage, string lang);
        DtoClinicSingleLang GetClinicById(int id, string pathToImage, string lang);
        DtoPagedClinic GetPagedClinics(int perPage, int pageNum, string pathToImage, string lang);
        DtoPagedClinic SearchClinics(int perPage, int numPage, DtoClinicSearchModel searchModel, string pathToImage, string lang);
        void Add(Domain.Entities.Clinic clinic);
        void Update(int id, string name);
        void Delete(int id);
    }
}
