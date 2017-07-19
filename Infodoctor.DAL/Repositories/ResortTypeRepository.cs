using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ResortTypeRepository : IResortTypeRepository
    {
        private readonly AppDbContext _context;

        public ResortTypeRepository(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public ResortType GeType(int id)
        {
            return _context.ResortTypes.First(type => type.Id == id);
        }

        public IQueryable<ResortType> GetTypes()
        {
            return _context.ResortTypes.OrderBy(type => type.Id);
        }

        public void Add(ResortType ct)
        {
            _context.ResortTypes.Add(ct);
            _context.SaveChanges();
        }

        public void Update(ResortType ct)
        {
            var updated = _context.ResortTypes.First(type => type.Id == ct.Id);
            updated = ct;
        }

        public void Delete(ResortType ct)
        {
            _context.ResortTypes.Remove(ct);
            _context.SaveChanges();
        }
    }
}
