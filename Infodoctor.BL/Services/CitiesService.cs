using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
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

        public IEnumerable<DtoCity> GetAllCities()
        {
            var cities = _citiesRepository.GetAllCities();
            var dtoCities = new List<DtoCity>();

            foreach (var city in cities)
            {
                var newDtoCity = new DtoCity() { Id = city.Id, Name = city.Name };
                dtoCities.Add(newDtoCity);
            }

            return dtoCities;
        }

        public IEnumerable<DtoCity> GetCitiesWithClinics()
        {
            var cities = _citiesRepository.GetAllCitiesWithClinics();
            var filtredCities = new List<DtoCity>();

            foreach (var city in cities)
            {
                filtredCities.Add(new DtoCity() { Id = city.Id, Name = city.Name });
            }

            return filtredCities;
        }

        public DtoCity GetCityById(int id)
        {
            var city = _citiesRepository.GetCityById(id);
            var dtoCity = new DtoCity() { Id = city.Id, Name = city.Name };
            return dtoCity;
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            _citiesRepository.Add(new City { Name = name });

            //var city = new City { Name = name };
            //if (IsNewElement(city))
            //    _citiesRepository.Add(city);
        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            var updated = _citiesRepository.GetCityById(id);
            if (updated != null)
            {
                updated.Name = name;
                _citiesRepository.Update(updated);
                //if (IsNewElement(updated))
                //    _citiesRepository.Update(updated);
            }
        }

        public void Delete(int id)
        {
            var city = _citiesRepository.GetCityById(id);
            if (city != null)
                _citiesRepository.Delete(city);
        }

        private bool IsNewElement(City city)
        {
            var cities = _citiesRepository.GetAllCities().ToList();
            foreach (var element in cities)
                if (element.Name.ToUpper() == city.Name.ToUpper() && element.Id != city.Id)
                    return false;
            return true;
        }
    }
}
