using System;
using Infodoctor.DAL.Interfaces.AddressInterfaces;

namespace Infodoctor.BL.Services
{
    public class AddressService
    {
        private readonly ICitiesRepository _citiesRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;

        public AddressService(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
        }

    }
}
