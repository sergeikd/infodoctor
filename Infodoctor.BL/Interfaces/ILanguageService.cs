using System.Collections.Generic;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Interfaces
{
    public interface ILanguageService
    {
        Language GetLanguageById(int id);
        Language GetLanguageByCode(string code);
        List<Language> GetLanguages();
    }
}
