﻿using System;
using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;

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

        public IEnumerable<DtoCity> GetAllCities(string lang)
        {
            var cities = _citiesRepository.GetAllCities();
            var dtoCities = new List<DtoCity>();

            foreach (var city in cities)
            {
                var dtoLocalizedCities = new List<LocalizedDtoCity>();
                foreach (var cityLocalisedCity in city.LocalisedCities)
                {
                    dtoLocalizedCities.Add(new LocalizedDtoCity()
                    {
                        Id = cityLocalisedCity.Id,
                        Name = cityLocalisedCity.Name,
                        LangCode = cityLocalisedCity.Language.Code
                    });
                }

                dtoCities.Add(new DtoCity() { Id = city.Id, LocalizedDtoCity = dtoLocalizedCities });
            }

            return dtoCities;
        }

        public IEnumerable<DtoCity> GetCitiesWithClinics(string lang)
        {
            var cities = _citiesRepository.GetAllCitiesWithClinics();
            var dtoCities = new List<DtoCity>();

            foreach (var city in cities)
            {
                var dtoLocalizedCities = new List<LocalizedDtoCity>();
                foreach (var cityLocalisedCity in city.LocalisedCities)
                {
                    dtoLocalizedCities.Add(new LocalizedDtoCity()
                    {
                        Id = cityLocalisedCity.Id,
                        Name = cityLocalisedCity.Name,
                        LangCode = cityLocalisedCity.Language.Code
                    });
                }

                dtoCities.Add(new DtoCity() { Id = city.Id, LocalizedDtoCity = dtoLocalizedCities });
            }

            return dtoCities;
        }

        public DtoCity GetCityById(int id, string lang)
        {
            var city = _citiesRepository.GetCityById(id);
            var dtoLocalizedCities = new List<LocalizedDtoCity>();
            foreach (var cityLocalisedCity in city.LocalisedCities)
            {
                dtoLocalizedCities.Add(new LocalizedDtoCity()
                {
                    Id = cityLocalisedCity.Id,
                    Name = cityLocalisedCity.Name,
                    LangCode = cityLocalisedCity.Language.Code
                });
            }

            var dtoCity = new DtoCity() { Id = city.Id, LocalizedDtoCity = dtoLocalizedCities };
            return dtoCity;
        }

        public void Add(string name, string lang)
        {
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name));
            //_citiesRepository.Add(new City {  });
        }

        public void Update(int id, string name, string lang)
        {
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name));
            //var updated = _citiesRepository.GetCityById(id);
            //if (updated != null)
            //{
            //    for (var i = 0; i < updated.LocalisedCities.Count; i++)

            //        if (updated.LocalisedCities.ToArray()[i].Language.Code.ToLower() == lang.ToLower())
            //            updated.LocalisedCities.ToArray()[i].Name = name;

            //    _citiesRepository.Update(updated);
            //}
        }

        public void Delete(int id)
        {
            var city = _citiesRepository.GetCityById(id);
            if (city != null)
                _citiesRepository.Delete(city);
        }
    }
}
