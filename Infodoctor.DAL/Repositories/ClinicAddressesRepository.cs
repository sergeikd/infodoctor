using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicAddressesRepository : IClinicAddressesRepository
    {
        private readonly AppDbContext _context;

        public ClinicAddressesRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Address> GetAllAddresses()
        {
            return _context.ClinicAddresses;
        }

        public Address GetAddress(int id)
        {
            return _context.ClinicAddresses.First(a => a.Id == id);
        }

        public void Add(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            _context.ClinicAddresses.Add(address);
            _context.SaveChanges();
        }

        public void Update(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            var updated = _context.ClinicAddresses.First(a => a.Id == address.Id);
            updated = address;
        }

        public void Delete(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            _context.ClinicAddresses.Remove(address);
            _context.SaveChanges();
        }
    }

    public class LocalizedClinicAddressesRepository : ILocalizedClinicAddressesRepository
    {
        private readonly AppDbContext _context;

        public LocalizedClinicAddressesRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<LocalizedAddress> GetAllLocalizedAddresses()
        {
            return _context.LocalizedClinicAddress;
        }

        public LocalizedAddress GetLocalizedAddress(int id)
        {
            return _context.LocalizedClinicAddress.First(a => a.Id == id);
        }

        public void Add(LocalizedAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            _context.LocalizedClinicAddress.Add(address);
            _context.SaveChanges();
        }

        public void Update(LocalizedAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            var updated = _context.LocalizedClinicAddress.First(a => a.Id == address.Id);
            updated = address;
        }

        public void Delete(LocalizedAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            _context.LocalizedClinicAddress.Remove(address);
            _context.SaveChanges();
        }
    }
}
