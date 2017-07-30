using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ResortTypeService : IResortTypeService
    {
        private readonly IResortTypeRepository _typeRepository;

        public ResortTypeService(IResortTypeRepository typeRepository)
        {
            if (typeRepository == null) throw new ArgumentNullException(nameof(typeRepository));
            _typeRepository = typeRepository;
        }

        public IEnumerable<DtoResortTypeSingleLang> GetTypes(string lang)
        {
            var types = _typeRepository.GetTypes().ToList();
            var dtoTypes = types.Select(type => ConvertEntityToDto(type, lang.ToLower())).ToList();
            return dtoTypes;
        }

        public DtoResortTypeSingleLang GetType(int id, string lang)
        {
            var type = _typeRepository.GeType(id);
            var dtoType = ConvertEntityToDto(type, lang.ToLower());
            return dtoType;
        }

        public DtoResortTypeMultiLang GetType(int id)
        {
            var type = _typeRepository.GeType(id);
            var locals = type.Localized.Select(clinicType => new DtoResortTypeLocalized()
            {
                Id = clinicType.Id,
                Name = clinicType.Name,
                LangCode = clinicType.Language.Code.ToLower()
            })
                .ToList();

            var resortsIdList = type.Resorts.Select(clinic => clinic.Id).ToList();
            var dtoType = new DtoResortTypeMultiLang() { Id = type.Id, ResortsIdList = resortsIdList, Localized = locals };
            return dtoType;
        }

        public void Add(DtoResortTypeMultiLang type)
        {
            throw new NotImplementedException();
        }

        public void Update(DtoResortTypeMultiLang type)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var type = _typeRepository.GeType(id);
            _typeRepository.Delete(type);
        }

        private static DtoResortTypeSingleLang ConvertEntityToDto(ResortType type, string lang)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(lang))
                throw new ArgumentNullException(nameof(lang));

            var localized = new LocalizedResortType();
            foreach (var clinicType in type.Localized)
                if (clinicType.Language.Code.ToLower() == lang)
                    localized = clinicType;

            var resortsIdList = type.Resorts.Select(clinic => clinic.Id).ToList();

            var dtoClinicType = new DtoResortTypeSingleLang()
            {
                Id = type.Id,
                Name = localized.Name,
                LangCode = localized.Language.Code.ToLower(),
                ResortsIdList = resortsIdList
            };

            return dtoClinicType;
        }

    }
}
