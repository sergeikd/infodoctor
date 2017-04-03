using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ArticleCommentsRepository : IArticleCommentsRepository
    {
        private readonly IAppDbContext _context;

        public ArticleCommentsRepository(IAppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<ArticleComment> GetAllArticleComments()
        {
            return _context.ArticleComments;
        }

        public IQueryable<ArticleComment> GetCommentsByArticleId(int id)
        {
            return _context.ArticleComments.Where(c => c.Article.Id == id).OrderByDescending(c => c.PublishTime);
        }

        public ArticleComment GetCommentById(int id)
        {
            return _context.ArticleComments.First(c => c.Id == id);
        }

        public void Add(ArticleComment comment)
        {
            _context.ArticleComments.Add(comment);
            _context.SaveChanges();
        }

        public void Update(ArticleComment comment)
        {
            var updated = _context.ArticleComments.First(c => c.Id == comment.Id);
            updated = comment;
            _context.SaveChanges();
        }

        public void Delete(ArticleComment comment)
        {
            _context.ArticleComments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
