using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain;

namespace Infodoctor.BL.Intefaces
{
    public interface IClinicService
    {
        IEnumerable<DtoClinic> GetAllClinics();
        DtoClinic GetClinicById(int id);
        void Add(Clinic clinic);
        void Update(int id, string name);
        void Delete(int id);
    }
}
