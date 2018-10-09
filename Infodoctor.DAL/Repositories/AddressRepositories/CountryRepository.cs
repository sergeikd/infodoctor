using System;
using System.Linq;
using Infodoctor.DAL.Interfaces.AddressInterfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories.AddressRepositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
            if(country == null)
                throw new ArgumentNullException();
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public void Update(Country country)
        {
            if (country == null)
                throw new ArgumentNullException();
            var updated = _context.Countries.First(c => c.Id == country.Id);
            updated = country;
            _context.SaveChanges();
        }

        public void Delete(Country country)
        {
            if (country == null)
                throw new ArgumentNullException();
            _context.Countries.Remove(country);
            _context.SaveChanges();
        }
    }
}
