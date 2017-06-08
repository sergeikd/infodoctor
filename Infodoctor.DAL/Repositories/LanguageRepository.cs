using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext _context;

        public LanguageRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Language GetLanguageById(int id)
        {
            return _context.Languages.First(l => l.Id == id);
        }

        public Language GetLanguageByCode(string code)
        {
            return _context.Languages.First(l => l.Code.ToLower() == code.ToLower());
        }

        public IQueryable<Language> GetLanguages()
        {
            return _context.Languages;
        }
    }
}
