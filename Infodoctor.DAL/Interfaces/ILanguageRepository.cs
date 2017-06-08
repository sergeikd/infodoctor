using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface ILanguageRepository
    {
        Language GetLanguageById(int id);
        Language GetLanguageByCode(string code);
        IQueryable<Language> GetLanguages();
    }
}
