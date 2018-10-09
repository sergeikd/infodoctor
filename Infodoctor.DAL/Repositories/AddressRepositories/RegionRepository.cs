using System;
using System.Linq;
using Infodoctor.DAL.Interfaces.AddressInterfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories.AddressRepositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _context;

        public RegionRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Region> GetAllRegions()
        {
            return _context.Regions;
        }

        public Region GetRegionById(int id)
        {
            return _context.Regions.First(c => c.Id == id);
        }

        public void Add(Region region)
        {
            if (region == null) throw new ArgumentNullException();
            _context.Regions.Add(region);
            _context.SaveChanges();
        }

        public void Update(Region region)
        {
            if (region == null) throw new ArgumentNullException();
            var updated = _context.Regions.First(c => c.Id == region.Id);
            updated = region;
            _context.SaveChanges();
        }

        public void Delete(Region region)
        {
            if (region == null) throw new ArgumentNullException();
            _context.Regions.Remove(region);
            _context.SaveChanges();
        }
    }
}
