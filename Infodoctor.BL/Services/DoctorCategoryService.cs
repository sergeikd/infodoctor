using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
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

        public IEnumerable<DtoDoctorCategorySingleLang> GetAllCategories(string lang)
        {
            var dcList = _doctorCategoryRepository.GetAllCategories().ToList();
            var dtoDcList = new List<DtoDoctorCategorySingleLang>();

            foreach (var dc in dcList)
            {
                var local = new LocalizedDoctorCategory();
                if (dc.Localized != null)
                    foreach (var category in dc.Localized)
                        local = string.Equals(category.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase) ? category : null;

                var dtoDc = new DtoDoctorCategorySingleLang()
                {
                    Id = dc.Id,
                    Name = local?.Name,
                    LangCode = local?.Language.Code.ToLower(),
                    Doctors = new List<int>()
                };

                foreach (var doctor in dc.Doctors)
                    dtoDc.Doctors.Add(doctor.Id);

                dtoDcList.Add(dtoDc);
            }

            return dtoDcList;
        }

        public DtoDoctorCategorySingleLang GetCategoryById(int id, string lang)
        {
            var dc = _doctorCategoryRepository.GetCategoryById(id);

            var local = new LocalizedDoctorCategory();
            if (dc.Localized != null)
                foreach (var category in dc.Localized)
                    local = string.Equals(category.Language.Code, lang, StringComparison.CurrentCultureIgnoreCase) ? category : null;

            var dtoDc = new DtoDoctorCategorySingleLang()
            {
                Id = dc.Id,
                Name = local?.Name,
                LangCode = local?.Language.Code.ToLower(),
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
            throw new NotImplementedException();
            //_doctorCategoryRepository.Add(new DoctorCategory() { Name = name });
        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            throw new NotImplementedException();
            //var dc = _doctorCategoryRepository.GetCategoryById(id);
            //if (dc != null)
            //{
            //    dc.Name = name;
            //    _doctorCategoryRepository.Update(dc);
            //}
        }

        public void Delete(int id)
        {
            var dc = _doctorCategoryRepository.GetCategoryById(id);
            if (dc != null)
                _doctorCategoryRepository.Delete(dc);
        }
    }
}
