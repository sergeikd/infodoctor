using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;

namespace Infodoctor.BL.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            if (countryRepository == null)
            {
                throw new ArgumentNullException(nameof(countryRepository));
            }
            _countryRepository = countryRepository;
        }
        public IEnumerable<Country> GetAllCountries()
        {
            return _countryRepository.GetAllCountries().ToList();
        }
    }
}
