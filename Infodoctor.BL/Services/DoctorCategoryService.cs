using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<DoctorCategory> GetAllCategories()
        {
            return _doctorCategoryRepository.GetAllCategories().ToList();
        }

        public DoctorCategory GetCategoryById(int id)
        {
            return _doctorCategoryRepository.GetCategoryById(id);
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
