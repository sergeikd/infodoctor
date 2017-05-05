using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IResortRepository
    {
        IQueryable<Resort> GetAllResorts();
        IQueryable<Resort> GetSortedResorts(string sortBy, bool descending);
        Resort GetResortById(int id);
        void Add(Resort res);
        void Update(Resort res);
        void Delete(Resort res);
    }
}
