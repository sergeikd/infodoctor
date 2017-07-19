using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicTypeRepository
    {
        ClinicType GeType(int id);
        IQueryable<ClinicType> GetTypes();
        void Add(ClinicType ct);
        void Update(ClinicType ct);
        void Delete(ClinicType ct);

    }
}
