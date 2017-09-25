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

            return cities.Select(city => ConvertEntityToDtoSingleLang(city, lang.ToLower())).ToList();
        }

        public IEnumerable<DtoCitySingleLang> GetCitiesWithClinics(string lang)
        {
            var cities = _citiesRepository.GetAllCitiesWithClinics().ToList();

            return cities.Select(city => ConvertEntityToDtoSingleLang(city, lang.ToLower())).ToList();
        }

        public DtoCitySingleLang GetCity(int id, string lang)
        {
            var city = _citiesRepository.GetCityById(id);
            var dtoCity = ConvertEntityToDtoSingleLang(city, lang.ToLower());
            return dtoCity;
        }

        public DtoCitySingleLang GetCity(string name, string lang)
        {
            lang = lang.ToLower();
            name = name.ToLower();

            var allCities = _citiesRepository.GetAllCities().ToList();
            var dtoType = new DtoCitySingleLang();

            foreach (var city in allCities)
                foreach (var localizedCity in city.LocalizedCities)
                    if (localizedCity.Language.Code.ToLower() == lang)
                        if (localizedCity.Name.ToLower().Contains(name) || name.Contains(localizedCity.Name.ToLower()))
                            dtoType = ConvertEntityToDtoSingleLang(city, lang);

            return dtoType;
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
            //var updated = _citiesRepository.GetCity(id);
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

        private static DtoCitySingleLang ConvertEntityToDtoSingleLang(City city, string lang)
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
                //CountryId = city.Country.Id
                CountryId = city.District.Region.Country.Id
            };
            return dtoCity;
        }
    }
}
