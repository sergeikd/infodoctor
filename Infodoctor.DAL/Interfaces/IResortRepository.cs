using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IResortRepository
    {
        IQueryable<Resort> GetAllResorts(int type);
        IQueryable<Resort> GetSortedResorts(string sortBy, bool descending,string lang, int type);
        Resort GetResortById(int id);
        void Add(Resort res);
        void Update(Resort res);
        void Delete(Resort res);
    }
}
