using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ResortRepository : IResortRepository
    {
        private readonly IAppDbContext _context;

        public ResortRepository(IAppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<Resort> GetAllResorts()
        {
            return _context.Resorts.OrderBy(r => r.Id);
        }

        public IQueryable<Resort> GetSortedResorts(string sortBy, bool @descending)
        {
            switch (sortBy)
            {

                default:
                    return descending ? _context.Resorts.OrderByDescending(c => c.Name) : _context.Resorts.OrderBy(c => c.Name);
                case "alphabet":
                    return descending ? _context.Resorts.OrderByDescending(c => c.Name) : _context.Resorts.OrderBy(c => c.Name);
                case "rate":
                    return descending ? _context.Resorts.OrderByDescending(c => c.RateAverage) : _context.Resorts.OrderBy(c => c.RateAverage);
                case "price":
                    return descending ? _context.Resorts.OrderByDescending(c => c.RatePrice) : _context.Resorts.OrderBy(c => c.RatePrice);
            }
        }

        public Resort GetResortById(int id)
        {
            try
            {
                var result = _context.Resorts.First(r => r.Id == id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Add(Resort res)
        {
            _context.Resorts.Add(res);
            _context.SaveChanges();
        }

        public void Update(Resort res)
        {
            var updated = _context.Resorts.First(r => r.Id == res.Id);
            updated = res;
        }

        public void Delete(Resort res)
        {
            _context.Resorts.Remove(res);
            _context.SaveChanges();
        }
    }
}
