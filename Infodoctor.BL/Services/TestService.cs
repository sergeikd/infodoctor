using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class TestService : ITestService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;
        Random _rnd = new Random();

        public TestService (IClinicRepository clinicRepository, IClinicSpecializationRepository clinicSpecializationRepository, ICitiesRepository citiesRepository, IDoctorRepository doctorRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(citiesRepository));
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            if (citiesRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));

            _citiesRepository = citiesRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
        }

        public void Add100Clinics()
        {
            var clinic = new Clinic();
            var maxClinicId = _clinicRepository.GetAllСlinics().Max(r => r.Id);
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();
            var cityList = _citiesRepository.GetAllCities().ToList();
            var doctrorList = _doctorRepository.GetAllDoctors().ToList();
            
            for (var i = maxClinicId + 1; i <= maxClinicId + 101; i++)
            {
                clinic.Name = "TestClinic" + i;
                clinic.Email = "testclinic" + i + "@infodoctor.by";
                clinic.Site = "TestClinic" + i + ".by";
                clinic.Doctors = new List<Doctor>() {doctrorList[i%doctrorList.Count]};
                clinic.CityAddresses = new List<ClinicAddress>()
                {
                    new ClinicAddress(){
                        City = cityList[i % 5],
                        Country = "TestCountry" + i,
                        Street = "TestStreet" + i,
                        Phones = new List<ClinicPhone>() { new ClinicPhone() {Description = "ClinicPhone" + i, Number = i + " 00 00"}
                }}};
                var clinicSpecializations = new List<ClinicSpecialization>();
                for (var j = 0; j < _rnd.Next(10); j++)
                {
                    clinicSpecializations.Add(clinicSpecializationList[_rnd.Next(clinicSpecializationList.Count/5)]);
                }
                clinic.ClinicSpecializations = clinicSpecializations.GroupBy(x => x.Id).Select(y => y.First()).ToList();
                clinic.Favorite = i%10 == 0;
                _clinicRepository.Add(clinic);
            }
        }
        public void Add100Doctors()
        {
            var doctor = new Doctor();
            var maxDoctorId = _clinicRepository.GetAllСlinics().Max(r => r.Id);
        }

        public Clinic PrepareClinic()
        {
            var clinic = new Clinic();
            var clinicSpecializationList = _clinicSpecializationRepository.GetAllClinicSpecializations().ToList();
            var cityList = _citiesRepository.GetAllCities().ToList();
            var doctrorList = _doctorRepository.GetAllDoctors().ToList();
            clinic.Name = "";
            clinic.Email = "";
            clinic.Site = "";
            clinic.Doctors = new List<Doctor>();
            clinic.CityAddresses = new List<ClinicAddress>();
            clinic.ClinicSpecializations = new List<ClinicSpecialization>();
            clinic.Favorite = false;
            return clinic;
        }

    }
}
