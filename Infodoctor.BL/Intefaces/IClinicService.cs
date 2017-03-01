using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain;

namespace Infodoctor.BL.Intefaces
{
    public interface IClinicService
    {
        IEnumerable<DtoClinic> GetAllClinics();
        DtoPagedClinic GetPagedClinics(int perPage, int pageNum);
        DtoClinic GetClinicById(int id);
        void Add(Clinic clinic);
        void Update(int id, string name);
        void Delete(int id);
    }
}
