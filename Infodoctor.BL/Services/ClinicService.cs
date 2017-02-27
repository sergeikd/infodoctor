using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<Clinic> GetAllClinics()
        {
            var clinicList = _clinicRepository.GetAllСlinics().ToList();
            
            //var dtoClinic = new List<Clinic>();
            //foreach (var clinic in clinicList)
            //{
            //    var clinicPhoneList = new List<ClinicPhone>();
            //    var clinicAddressList = new List<ClinicAddress>();
            //    clinicAddressList = clinic.ClinicAddresses.ToList();
            //    dtoClinic.Add(new DtoClinic()
            //    {
            //        Id = clinic.Id,
            //        Name = clinic.Name,
            //        Email = clinic.Email,
            //        ClinicAddresses = clinicAddressList,
            //        ClinicSpecializations = clinic.ClinicSpecializations,
            //        ClinicProfiles = clinic.ClinicProfiles
            //    });
            //}
            return clinicList;
        }

        public Clinic GetClinicById(int id)
        {
            return _clinicRepository.GetClinicById(id);
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
