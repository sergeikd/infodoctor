using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
        }

        public Language GetLanguageById(int id)
        {
            return _languageRepository.GetLanguageById(id);
        }

        public Language GetLanguageByCode(string code)
        {
            return _languageRepository.GetLanguageByCode(code);
        }

        public List<Language> GetLanguages()
        {
            return _languageRepository.GetLanguages().ToList();
        }
    }
}
