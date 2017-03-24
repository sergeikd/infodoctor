using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface IClinicService
    {
        IEnumerable<DtoClinic> GetAllClinics(string pathToImage);
        DtoPagedClinic GetPagedClinics(int perPage, int pageNum, string pathToImage);
        DtoPagedClinic SearchClinics(int perPage, int numPage, DtoClinicSearchModel searchModel, string pathToImage);
        DtoClinic GetClinicById(int id, string pathToImage);
        void Add(Domain.Entities.Clinic clinic);
        void Update(int id, string name);
        void Delete(int id);
    }
}
