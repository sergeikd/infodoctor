using System;
using System.Linq;
using Infodoctor.DAL.Interfaces.AddressInterfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories.AddressRepositories
{
    public class CitiyRepository : ICitiesRepository
    {
        private readonly AppDbContext _context;

        public CitiyRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<City> GetAllCities()
        {
            return _context.Cities;
        }

        public IQueryable<City> GetAllCitiesWithClinics()
        {
            return _context.Cities.Where(c => c.Adresses.Any());
        }

        public City GetCityById(int id)
        {
            return _context.Cities.First(c => c.Id == id);
        }

        public void Add(City city)
        {
            if (city == null)
                throw new ArgumentNullException();
            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public void Update(City city)
        {
            if (city == null)
                throw new ArgumentNullException();
            var updated = _context.Cities.First(c => c.Id == city.Id);
            updated = city;
            _context.SaveChanges();
        }

        public void Delete(City city)
        {
            if (city == null)
                throw new ArgumentNullException();
            _context.Cities.Remove(city);
            _context.SaveChanges();
        }
    }
}
