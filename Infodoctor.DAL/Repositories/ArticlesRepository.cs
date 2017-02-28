using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly IAppDbContext _context;

        public ArticlesRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<Article> GetAllArticles()
        {
            return _context.Articles;
        }

        public Article GetArticleById(int id)
        {
            return _context.Articles.First(a => a.Id == id);
        }

        public void Add(Article art)
        {
            _context.Articles.Add(art);
            _context.SaveChanges();
        }

        public void Update(Article art)
        {
            var edited = _context.Articles.First(a => a.Id == art.Id);
            edited = art;
            _context.SaveChanges();
        }

        public void Delete(Article art)
        {
            _context.Articles.Remove(art);
            _context.SaveChanges();
        }
    }
}
