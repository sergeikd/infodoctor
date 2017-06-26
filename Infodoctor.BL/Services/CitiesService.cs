using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesService(ICitiesRepository citiesRepository)
        {
            if (citiesRepository == null)
                throw new ArgumentNullException(nameof(citiesRepository));
            _citiesRepository = citiesRepository;
        }

        public IEnumerable<DtoCitySingleLang> GetAllCities(string lang)
        {
            lang = lang.ToLower();

            var cities = _citiesRepository.GetAllCities().ToList();
            var dtoCities = new List<DtoCitySingleLang>();

            foreach (var city in cities)
            {
                var dtoCity = ConvertEntityToDtoSingleLang(lang, city);
                dtoCities.Add(dtoCity);
            }

            return dtoCities;
        }

        public IEnumerable<DtoCitySingleLang> GetCitiesWithClinics(string lang)
        {
            lang = lang.ToLower();

            var cities = _citiesRepository.GetAllCitiesWithClinics();
            var dtoCities = new List<DtoCitySingleLang>();

            foreach (var city in cities)
            {
                var dtoCity = ConvertEntityToDtoSingleLang(lang, city);
                dtoCities.Add(dtoCity);
            }

            return dtoCities;
        }

        public DtoCitySingleLang GetCityById(int id, string lang)
        {
            lang = lang.ToLower();

            var city = _citiesRepository.GetCityById(id);

            var dtoCity = ConvertEntityToDtoSingleLang(lang,city);

            return dtoCity;
        }

        public void Add(DtoCityMultiLang city)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name));
            //_citiesRepository.Add(new City {  });
        }

        public void Update(DtoCityMultiLang city)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name));
            //var updated = _citiesRepository.GetCityById(id);
            //if (updated != null)
            //{
            //    for (var i = 0; i < updated.LocalizedCities.Count; i++)

            //        if (updated.LocalizedCities.ToArray()[i].Language.Code.ToLower() == lang.ToLower())
            //            updated.LocalizedCities.ToArray()[i].Name = name;

            //    _citiesRepository.Update(updated);
            //}
        }

        public void Delete(int id)
        {
            var city = _citiesRepository.GetCityById(id);
            if (city != null)
                _citiesRepository.Delete(city);
        }

        private static DtoCitySingleLang ConvertEntityToDtoSingleLang(string lang, City city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));

            LocalizedCity localizedCity = null;
            try
            {
                localizedCity = city.LocalizedCities.First(l => l.Language.Code.ToLower() == lang);
            }
            catch (Exception)
            {
                // ignored
            }

            var dtoCity = new DtoCitySingleLang()
            {
                Id = city.Id,
                Name = localizedCity?.Name,
                LangCode = localizedCity?.Language.Code.ToLower(),
                CountryId = city.Country.Id
            };
            return dtoCity;
        }
    }
}
