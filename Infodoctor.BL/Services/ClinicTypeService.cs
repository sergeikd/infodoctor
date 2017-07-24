using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ClinicTypeService : IClinicTypeService
    {
        private readonly IClinicTypeRepository _typeRepository;

        public ClinicTypeService(IClinicTypeRepository typeRepository)
        {
            if (typeRepository == null) throw new ArgumentNullException(nameof(typeRepository));
            _typeRepository = typeRepository;
        }

        public IEnumerable<DtoClinicTypeSingleLang> GetTypes(string lang)
        {
            var types = _typeRepository.GetTypes().ToList();
            var dtoTypes = types.Select(type => ConvertEntityToDto(type, lang.ToLower())).ToList();
            return dtoTypes;
        }

        public DtoClinicTypeSingleLang GetType(int id, string lang)
        {
            var type = _typeRepository.GeType(id);
            var dtoType = ConvertEntityToDto(type, lang.ToLower());
            return dtoType;
        }

        public DtoClinicTypeSingleLang GetType(string name, string lang)
        {
            lang = lang.ToLower();
            name = name.ToLower();

            var types = _typeRepository.GetTypes().ToList();
            var dtoType = new DtoClinicTypeSingleLang();

            foreach (var type in types)
                foreach (var localizedClinicType in type.Localized)
                    if (localizedClinicType.Language.Code.ToLower() == lang)
                        if (localizedClinicType.Name.ToLower() == name)
                            dtoType = ConvertEntityToDto(type, lang);

            return dtoType;
        }

        public DtoClinicTypeMultiLang GetType(int id)
        {
            var type = _typeRepository.GeType(id);

            var locals = type.Localized.Select(clinicType => new DtoClinicTypeLocalized()
            {
                Id = clinicType.Id,
                Name = clinicType.Name,
                LangCode = clinicType.Language.Code.ToLower()
            })
                .ToList();

            var clinicsIdList = type.Clinics.Select(clinic => clinic.Id).ToList();
            var dtoType = new DtoClinicTypeMultiLang() { Id = type.Id, ClinicsIdList = clinicsIdList, Localized = locals };
            return dtoType;
        }

        public void Add(DtoClinicTypeMultiLang type)
        {
            throw new NotImplementedException();
        }

        public void Update(DtoClinicTypeMultiLang type)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var type = _typeRepository.GeType(id);
            _typeRepository.Delete(type);
        }

        private static DtoClinicTypeSingleLang ConvertEntityToDto(ClinicType type, string lang)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(lang))
                throw new ArgumentNullException(nameof(lang));

            var localized = new LocalizedClinicType();
            foreach (var clinicType in type.Localized)
                if (clinicType.Language.Code.ToLower() == lang)
                    localized = clinicType;

            var clinicsIdList = type.Clinics.Select(clinic => clinic.Id).ToList();

            var dtoClinicType = new DtoClinicTypeSingleLang()
            {
                Id = type.Id,
                Name = localized.Name,
                LangCode = localized.Language.Code.ToLower(),
                ClinicsIdList = clinicsIdList
            };

            return dtoClinicType;
        }
    }
}
