using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IArticleCommentsRepository
    {
        IQueryable<ArticleComment> GetAllArticleComments();
        IQueryable<ArticleComment> GetCommentsByArticleId(int id);
        ArticleComment GetCommentById(int id);
        void Add(ArticleComment comment);
        void Update(ArticleComment comment);
        void Delete(ArticleComment comment);
    }
}
