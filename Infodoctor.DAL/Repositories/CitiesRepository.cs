using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Repositories
{
    public class CitiesRepository: ICitiesRepository
    {
        private readonly IAppDbContext _context;

        public CitiesRepository(IAppDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
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
            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public void Update(City city)
        {
            var updated = _context.Cities.First(c => c.Id == city.Id);
            updated = city;
        }

        public void Delete(City city)
        {
            _context.Cities.Remove(city);
            _context.SaveChanges();
        }
    }
}
