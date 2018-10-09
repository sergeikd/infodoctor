using System;
using System.Linq;
using Infodoctor.DAL.Interfaces.AddressInterfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories.AddressRepositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly AppDbContext _context;

        public DistrictRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<District> GetAllDistricts()
        {
            return _context.Districts;
        }

        public District GetDistrictById(int id)
        {
            return _context.Districts.First(c => c.Id == id);
        }

        public void Add(District district)
        {
            if (district == null)
                throw new ArgumentNullException();
            _context.Districts.Add(district);
            _context.SaveChanges();
        }

        public void Update(District district)
        {
            if (district == null)
                throw new ArgumentNullException();
            var updated = _context.Districts.First(c => c.Id == district.Id);
            updated = district;
            _context.SaveChanges();
        }

        public void Delete(District district)
        {
            if (district == null)
                throw new ArgumentNullException();
            _context.Districts.Remove(district);
            _context.SaveChanges();
        }
    }
}

