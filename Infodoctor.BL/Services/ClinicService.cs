using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;

namespace Infodoctor.BL.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IСlinicRepository _clinicRepository;

        public ClinicService(IСlinicRepository clinicRepository)
        {
            if (clinicRepository == null)
            {
                throw new ArgumentNullException(nameof(clinicRepository));
            }
            _clinicRepository = clinicRepository;
        }

        public IEnumerable<DtoClinic> GetAllClinics()
        {
            var clinicList = _clinicRepository.GetAllСlinics().ToList();

            var dtoClinicList = new List<DtoClinic>();
            foreach (var clinic in clinicList)
            {
                var dtoClinic = new DtoClinic
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Email = clinic.Email
                };
                var dtoClinicAddressList = new List<DtoClinicAddress>();
                foreach (var clinicAddress in clinic.ClinicAddresses)
                {
                    var dtoClinicAddress = new DtoClinicAddress
                    {
                        ClinicAddress = clinicAddress.Address,
                        ClinicPhone = new List<Dictionary<string, string>>()
                    };
                    foreach (var dtoClinicPhone in clinicAddress.ClinicPhones.Select(clinicPhone => new Dictionary<string, string> { { clinicPhone.Description, clinicPhone.Number } }))
                    {
                        dtoClinicAddress.ClinicPhone.Add(dtoClinicPhone);
                    }
                    dtoClinicAddressList.Add(dtoClinicAddress);
                }
                dtoClinic.ClinicAddress = dtoClinicAddressList;

                var dtoSpecializationList = clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
                dtoClinic.ClinicSpecialization = dtoSpecializationList;

                dtoClinicList.Add(dtoClinic);
            }
            return dtoClinicList;
        }

        public DtoClinic GetClinicById(int id)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new ApplicationException("Clinic not found");
            }
            var dtoClinic = new DtoClinic
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Email = clinic.Email
            };
            var dtoClinicAddressList = new List<DtoClinicAddress>();
            foreach (var clinicAddress in clinic.ClinicAddresses)
            {
                var dtoClinicAddress = new DtoClinicAddress
                {
                    ClinicAddress = clinicAddress.Address,
                    ClinicPhone = new List<Dictionary<string, string>>()
                };
                foreach (var dtoClinicPhone in clinicAddress.ClinicPhones.Select(clinicPhone => new Dictionary<string, string> {{clinicPhone.Description, clinicPhone.Number}}))
                {
                    dtoClinicAddress.ClinicPhone.Add(dtoClinicPhone);
                }
                dtoClinicAddressList.Add(dtoClinicAddress);
            }
            dtoClinic.ClinicAddress = dtoClinicAddressList;

            var dtoSpecializationList = clinic.ClinicSpecializations.Select(specialization => specialization.Name).ToList();
            dtoClinic.ClinicSpecialization = dtoSpecializationList;

            return dtoClinic;
        }

        public void Add(Clinic clinic)
        {
            if (clinic == null )
                throw new ArgumentNullException(nameof(clinic));

            var c = clinic;

            _clinicRepository.Add(c);
        }


        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var c = _clinicRepository.GetClinicById(id);
            if (c != null)
            {
                c.Name = name;

                _clinicRepository.Update(c);
            }
        }

        public void Delete(int id)
        {
            var cp = _clinicRepository.GetClinicById(id);

            if (cp != null)
                _clinicRepository.Delete(cp);
        }
    }
}
