using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicPhonesRepository
    {
        IQueryable<Phone> GetAllPhones();
        Phone GetPhone(int id);
        void Add(Phone phone);
        void Update(Phone phone);
        void Delete(Phone phone);
    }

    public interface ILocalizedClinicPhonesRepository
    {
        IQueryable<LocalizedPhone> GetAllLocalizedPhones();
        LocalizedPhone GetLocalizedPhone(int id);
        void Add(LocalizedPhone phone);
        void Update(LocalizedPhone phone);
        void Delete(LocalizedPhone phone);
    }
}