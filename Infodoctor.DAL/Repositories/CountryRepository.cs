using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;

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
    }
}
