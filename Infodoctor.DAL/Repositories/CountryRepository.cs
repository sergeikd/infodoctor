using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IAppDbContext _context;

        public CountryRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<Country> GetAllCountries()
        {
            return _context.Countries;
        }

        public Country GetCountryById(int id)
        {
            return _context.Countries.First(c => c.Id == id);
        }

        public void Add(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public void Update(Country country)
        {
            var updated = _context.Countries.First(c => c.Id == country.Id);
            updated = country;
            _context.SaveChanges();
        }

        public void Delete(Country country)
        {
            _context.Countries.Remove(country);
            _context.SaveChanges();
        }
    }
}
