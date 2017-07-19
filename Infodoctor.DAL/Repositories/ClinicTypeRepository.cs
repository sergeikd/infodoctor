using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicTypeRepository : IClinicTypeRepository
    {
        private readonly AppDbContext _context;

        public ClinicTypeRepository(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public ClinicType GeType(int id)
        {
            return _context.ClinicTypes.First(type => type.Id == id);
        }

        public IQueryable<ClinicType> GetTypes()
        {
            return _context.ClinicTypes.OrderBy(type => type.Id);
        }

        public void Add(ClinicType ct)
        {
            _context.ClinicTypes.Add(ct);
            _context.SaveChanges();

        }

        public void Update(ClinicType ct)
        {
            var updated = _context.ClinicTypes.First(type => type.Id == ct.Id);
            updated = ct;
        }

        public void Delete(ClinicType ct)
        {
            _context.ClinicTypes.Remove(ct);
            _context.SaveChanges();
        }
    }
}
