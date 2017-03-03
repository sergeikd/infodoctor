using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ArticleThemesRepository : IArticleThemesRepository
    {
        private readonly IAppDbContext _context;

        public ArticleThemesRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<ArticleTheme> GetAllThemes()
        {
            return null;//_context.ArticleThemes;
        }

        public ArticleTheme GetThemeById(int id)
        {
            return null;//_context.ArticleThemes.First(at => at.Id == id);
        }


        public void Add(ArticleTheme artt)
        {
            //_context.ArticleThemes.Add(artt);
            //_context.SaveChanges();
        }

        public void Update(ArticleTheme artt)
        {
            //var edited = _context.ArticleThemes.First(at => at.Id == artt.Id);
            //edited = artt;
        }

        public void Delete(ArticleTheme artt)
        {
            //_context.ArticleThemes.Remove(artt);
            //_context.SaveChanges();
        }

    }
}
