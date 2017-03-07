using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class DoctorSpecializationService : IDoctorSpecializationService
    {
        private readonly IDoctorSpecializationRepository _doctorSpecializationRepository;

        public DoctorSpecializationService(IDoctorSpecializationRepository doctorSpecializationRepository)
        {
            if (doctorSpecializationRepository == null)
                throw new ArgumentNullException(nameof(doctorSpecializationRepository));
            _doctorSpecializationRepository = doctorSpecializationRepository;
        }

        public IEnumerable<DoctorSpecialization> GetAllSpecializations()
        {
            return _doctorSpecializationRepository.GetAllSpecializations().ToList();
        }

        public DoctorSpecialization GetSpecializationById(int id)
        {
            return _doctorSpecializationRepository.GetSpecializationById(id);
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            _doctorSpecializationRepository.Add(
                new DoctorSpecialization() { Name = name });

        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var ds = _doctorSpecializationRepository.GetSpecializationById(id);
            if (ds!=null)
            {
                ds.Name = name;
                _doctorSpecializationRepository.Update(ds);
            }
        }

        public void Delete(int id)
        {
            var ds = _doctorSpecializationRepository.GetSpecializationById(id);
            if (ds != null)
                _doctorSpecializationRepository.Delete(ds);
        }
    }
}
