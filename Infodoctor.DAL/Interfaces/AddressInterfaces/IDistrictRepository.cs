using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces.AddressInterfaces
{
    public interface IDistrictRepository
    {
        IQueryable<District> GetAllDistricts();
        District GetDistrictById(int id);
        void Add(District district);
        void Update(District district);
        void Delete(District district);
    }
}
