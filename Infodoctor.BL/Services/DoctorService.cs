﻿using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;
using static System.String;

namespace Infodoctor.BL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorSpecializationRepository _doctorSpecializationRepository;
        private readonly IDoctorCategoryRepository _doctorCategoryRepository;
        private readonly IСlinicRepository _clinicRepository;

        public DoctorService(IDoctorRepository doctorRepository, IDoctorSpecializationRepository doctorSpecializationRepository, IDoctorCategoryRepository doctorCategoryRepository, IСlinicRepository clinicRepository)
        {
            if (doctorRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));
            if (doctorCategoryRepository == null)
                throw new ArgumentNullException(nameof(doctorRepository));
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));

            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
            _doctorSpecializationRepository = doctorSpecializationRepository;
            _doctorCategoryRepository = doctorCategoryRepository;
        }

        public IEnumerable<DtoDoctor> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAllDoctors().ToList();
            var result = new List<DtoDoctor>();

            foreach (var doctor in doctors)
            {
                var dtoDoctor = new DtoDoctor()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Experience = doctor.Experience,
                    Manipulation = doctor.Manipulation,
                    Specialization = doctor.Specialization.Name,
                    Category = doctor.Category.Name
                };

                if (doctor.Address != null)
                {
                    var dtoAddress = new DtoAddress()
                    {
                        City = doctor.Address.City.Name,
                        Street = doctor.Address.Street,
                        ClinicPhones = new List<DtoPhone>()
                    };

                    foreach (var phone in doctor.Address.ClinicPhones)
                    {
                        var dtoPhone = new DtoPhone() { Desc = phone.Description, Phone = phone.Number };
                        dtoAddress.ClinicPhones.Add(dtoPhone);
                    }
                    dtoDoctor.Address = dtoAddress;
                }

                result.Add(dtoDoctor);
            }

            return result;
        }

        public DtoDoctor GetDoctorById(int id)
        {
            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor == null)
                throw new ApplicationException("Doctor not found");

            var dtoDoctor = new DtoDoctor()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Email = doctor.Email,
                Experience = doctor.Experience,
                Manipulation = doctor.Manipulation,
                Specialization = doctor.Specialization.Name,
                Category = doctor.Category.Name
            };

            if (doctor.Address != null)
            {
                var dtoAddress = new DtoAddress()
                {
                    City = doctor.Address.City.Name,
                    Street = doctor.Address.Street,
                    ClinicPhones = new List<DtoPhone>()
                };

                foreach (var phone in doctor.Address.ClinicPhones)
                {
                    var dtoPhone = new DtoPhone() { Desc = phone.Description, Phone = phone.Number };
                    dtoAddress.ClinicPhones.Add(dtoPhone);
                }
                dtoDoctor.Address = dtoAddress;
            }

            return dtoDoctor;
        }

        public void Add(DtoDoctor newDoctor)
        {
            if (newDoctor == null)
                throw new ArgumentNullException(nameof(newDoctor));

            var doctor = new Doctor()
            {
                Name = newDoctor.Name,
                Email = newDoctor.Email,
                Experience = newDoctor.Experience,
                Manipulation = newDoctor.Manipulation
            };

            var clinicsList = new List<Clinic>();

            foreach (var clinicId in newDoctor.ClinicsId)
            {
                var clinic = _clinicRepository.GetClinicById(clinicId);
                if (clinic != null)
                    clinicsList.Add(clinic);
            }

            doctor.Clinics = clinicsList;

            var doctorSpesList =
                _doctorSpecializationRepository.GetAllSpecializations().ToList();
            var doctorCategotyList = _doctorCategoryRepository.GetAllCategories().ToList();

            doctor.Specialization = doctorSpesList.First(ds => String.Equals(ds.Name, newDoctor.Specialization, StringComparison.CurrentCultureIgnoreCase));
            doctor.Category = doctorCategotyList.First(dc => String.Equals(dc.Name, newDoctor.Category, StringComparison.CurrentCultureIgnoreCase));


            _doctorRepository.Add(doctor);
        }

        public void Update(int id, DtoDoctor newDoctor)
        {
            if (newDoctor == null)
                throw new ArgumentNullException(nameof(newDoctor));

            var doctor = _doctorRepository.GetDoctorById(id);

            if (doctor != null)
            {
                doctor.Name = newDoctor.Name;
                doctor.Email = newDoctor.Email;
                doctor.Experience = newDoctor.Experience;
                doctor.Manipulation = newDoctor.Manipulation;

                var doctorSpesList =
                    _doctorSpecializationRepository.GetAllSpecializations().ToList();
                var doctorCategotyList = _doctorCategoryRepository.GetAllCategories().ToList();
                var clinicsList = new List<Clinic>();

                foreach (var clinicId in newDoctor.ClinicsId)
                {
                    var clinic = _clinicRepository.GetClinicById(clinicId);
                    if (clinic != null)
                        clinicsList.Add(clinic);
                }

                doctor.Clinics = clinicsList;
                doctor.Specialization = doctorSpesList.First(ds => string.Equals(ds.Name, newDoctor.Specialization, StringComparison.CurrentCultureIgnoreCase));
                doctor.Category = doctorCategotyList.First(dc => string.Equals(dc.Name, newDoctor.Category, StringComparison.CurrentCultureIgnoreCase));

                _doctorRepository.Update(doctor);
            }
        }

        public void Delete(int id)
        {
            var doctor = _doctorRepository.GetDoctorById(id);
            if (doctor != null)
                _doctorRepository.Delete(doctor);
        }
    }
}
