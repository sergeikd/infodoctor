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
        private readonly ILanguageRepository _languageRepository;

        public CountryService(ICountryRepository countryRepository, ILanguageRepository languageRepository)
        {
            if (countryRepository == null) throw new ArgumentNullException(nameof(countryRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));
            _countryRepository = countryRepository;
            _languageRepository = languageRepository;
        }

        public IEnumerable<DtoCountrySingleLang> GetAllCountries(string lang)
        {
            var counties = _countryRepository.GetAllCountries().ToList();
            var dtoCountries = new List<DtoCountrySingleLang>();
            foreach (var country in counties)
            {
                var dtoCounty = ConvertEntityToDtoSingleLang(lang, country);
                dtoCountries.Add(dtoCounty);
            }

            return dtoCountries;
        }

        public DtoCountrySingleLang GetCountryById(int id, string lang)
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

            var dtoCounty = ConvertEntityToDtoSingleLang(lang, country);

            return dtoCounty;
        }

        public void Add(DtoCountryMultiLang country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            var locals = new List<LocalizedCountry>();

            if (country.LocalizedCoutries != null)
                foreach (var localizedCoutry in country.LocalizedCoutries)
                {
                    locals.Add(new LocalizedCountry()
                    {
                        Name = localizedCoutry.Name,
                        Language = _languageRepository.GetLanguageByCode(localizedCoutry.LangCode)
                    });
                }

            var newCountry = new Country() { LocalizedCountries = locals };

            _countryRepository.Add(newCountry);
        }

        public void Update(DtoCountryMultiLang country)
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

            var locals = new List<LocalizedCountry>();

            if (country.LocalizedCoutries != null)
                foreach (var localizedCoutry in country.LocalizedCoutries)
                {
                    locals.Add(new LocalizedCountry()
                    {
                        Name = localizedCoutry.Name,
                        Language = _languageRepository.GetLanguageByCode(localizedCoutry.LangCode)
                    });
                }

            updated.LocalizedCountries = locals;

            _countryRepository.Update(updated);
        }

        public void Delete(int id)
        {
            var deleted = _countryRepository.GetCountryById(id);
            _countryRepository.Delete(deleted);
        }

        private static DtoCountrySingleLang ConvertEntityToDtoSingleLang(string lang, Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            var local = new LocalizedCountry();
            if (country.LocalizedCountries != null)
                foreach (var localizedCountry in country.LocalizedCountries)
                    if (string.Equals(localizedCountry.Language.Code.ToLower(), lang.ToLower(),
                        StringComparison.Ordinal))
                        local = localizedCountry;
            var cities = country.Cities.Select(city => city.Id).ToList();

            var dtoCounty = new DtoCountrySingleLang()
            {
                Id = country.Id,
                Name = local.Name,
                LangCode = local.Language?.Code.ToLower(),
                CitiesId = cities
            };

            return dtoCounty;
        }
    }
}
