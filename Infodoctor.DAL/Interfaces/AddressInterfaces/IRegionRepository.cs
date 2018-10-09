using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces.AddressInterfaces
{
    public interface IRegionRepository
    {
        IQueryable<Region> GetAllRegions();
        Region GetRegionById(int id);
        void Add(Region region);
        void Update(Region region);
        void Delete(Region region);
    }
}
