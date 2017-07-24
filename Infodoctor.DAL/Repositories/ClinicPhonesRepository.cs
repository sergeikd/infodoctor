using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicPhonesRepository : IClinicPhonesRepository
    {

        private readonly AppDbContext _context;

        public ClinicPhonesRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Phone> GetAllPhones()
        {
            return _context.ClinicPhones;
        }

        public Phone GetPhone(int id)
        {
            return _context.ClinicPhones.First(p => p.Id == id);
        }

        public void Add(Phone phone)
        {
            _context.ClinicPhones.Add(phone);
            _context.SaveChanges();
        }

        public void Update(Phone phone)
        {
            var updated = _context.ClinicPhones.First(c => c.Id == phone.Id);
            updated = phone;
        }

        public void Delete(Phone phone)
        {
            _context.ClinicPhones.Remove(phone);
            _context.SaveChanges();
        }
    }

    public class LocalizedClinicPhonesRepository : ILocalizedClinicPhonesRepository
    {

        private readonly AppDbContext _context;

        public LocalizedClinicPhonesRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<LocalizedPhone> GetAllLocalizedPhones()
        {
            return _context.LocalizedClinicPhone;
        }

        public LocalizedPhone GetLocalizedPhone(int id)
        {
            return _context.LocalizedClinicPhone.First(p => p.Id == id);
        }

        public void Add(LocalizedPhone phone)
        {
            _context.LocalizedClinicPhone.Add(phone);
            _context.SaveChanges();
        }

        public void Update(LocalizedPhone phone)
        {
            var updated = _context.LocalizedClinicPhone.First(c => c.Id == phone.Id);
            updated = phone;
        }

        public void Delete(LocalizedPhone phone)
        {
            _context.LocalizedClinicPhone.Remove(phone);
            _context.SaveChanges();
        }
    }
}
