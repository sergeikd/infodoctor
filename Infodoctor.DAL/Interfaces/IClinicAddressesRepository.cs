using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicAddressesRepository
    {
        IQueryable<Address> GetAllAddresses();
        Address GetAddress(int id);
        void Add(Address address);
        void Update(Address address);
        void Delete(Address address);
    }

    public interface ILocalizedClinicAddressesRepository
    {
        IQueryable<LocalizedAddress> GetAllLocalizedAddresses();
        LocalizedAddress GetLocalizedAddress(int id);
        void Add(LocalizedAddress address);
        void Update(LocalizedAddress address);
        void Delete(LocalizedAddress address);
    }
}
