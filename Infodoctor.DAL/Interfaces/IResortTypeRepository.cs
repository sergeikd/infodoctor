using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IResortTypeRepository
    {
        ResortType GeType(int id);
        IQueryable<ResortType> GetTypes();
        void Add(ResortType ct);
        void Update(ResortType ct);
        void Delete(ResortType ct);
    }
}
