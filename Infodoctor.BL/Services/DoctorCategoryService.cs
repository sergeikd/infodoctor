using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class DoctorCategoryService : IDoctorCategoryService
    {
        private readonly IDoctorCategoryRepository _doctorCategoryRepository;

        public DoctorCategoryService(IDoctorCategoryRepository doctorCategoryRepository)
        {
            if (doctorCategoryRepository == null)
                throw new ArgumentNullException(nameof(doctorCategoryRepository));
            _doctorCategoryRepository = doctorCategoryRepository;
        }

        public IEnumerable<DtoDoctorCategory> GetAllCategories()
        {
            var dcList = _doctorCategoryRepository.GetAllCategories().ToList();
            var dtoDcList = new List<DtoDoctorCategory>();

            foreach (var dc in dcList)
            {
                var dtoDc = new DtoDoctorCategory()
                {
                    Id = dc.Id,
                    Name = dc.Name,
                    Doctors = new List<int>()
                };

                foreach (var doctor in dc.Doctors)
                    dtoDc.Doctors.Add(doctor.Id);

                dtoDcList.Add(dtoDc);
            }

            return dtoDcList;
        }

        public DtoDoctorCategory GetCategoryById(int id)
        {
            var dc = _doctorCategoryRepository.GetCategoryById(id);

            var dtoDc = new DtoDoctorCategory()
            {
                Id = dc.Id,
                Name = dc.Name,
                Doctors = new List<int>()
            };

            foreach (var doctor in dc.Doctors)
            {
                dtoDc.Doctors.Add(doctor.Id);
            }
            return dtoDc;
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            _doctorCategoryRepository.Add(new DoctorCategory() { Name = name });
        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            var dc = _doctorCategoryRepository.GetCategoryById(id);
            if (dc != null)
            {
                dc.Name = name;
                _doctorCategoryRepository.Update(dc);
            }
        }

        public void Delete(int id)
        {
            var dc = _doctorCategoryRepository.GetCategoryById(id);
            if (dc != null)
                _doctorCategoryRepository.Delete(dc);
        }
    }
}
