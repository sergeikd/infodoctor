using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

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

        public IEnumerable<DtoCountry> GetAllCountries()
        {
            var counties = _countryRepository.GetAllCountries().ToList();
            var dtoCountries = new List<DtoCountry>();
            foreach (var country in counties)
            {
                dtoCountries.Add(new DtoCountry()
                {
                    Id = country.Id,
                    Name = country.Name
                });
            }

            return dtoCountries;
        }

        public DtoCountry GetCountryById(int id)
        {
            Country country;

            try
            {
                country = _countryRepository.GetCountryById(id);
            }
            catch
            {
                throw new ApplicationException("Country not found");
            }

            var dtoCounty = new DtoCountry()
            {
                Id = country.Id,
                Name = country.Name
            };

            return dtoCounty;
        }

        public void Add(DtoCountry country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            var newCountry = new Country()
            {
                Name = country.Name
            };
            _countryRepository.Add(newCountry);
        }

        public void Update(DtoCountry country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            Country updated;

            try
            {
                updated = _countryRepository.GetCountryById(country.Id);
            }
            catch
            {
                throw new ApplicationException("Country not found");
            }

            updated.Name = country.Name;


            _countryRepository.Update(updated);
        }

        public void Delete(int id)
        {
            var deleted = _countryRepository.GetCountryById(id);
            _countryRepository.Delete(deleted);
        }
    }
}
