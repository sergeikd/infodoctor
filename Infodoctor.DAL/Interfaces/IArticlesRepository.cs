using Infodoctor.Domain;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IArticlesRepository
    {
        IQueryable<Article> GetAllArticles();
        Article GetArticleById(int id);
        void Add(Article art);
        void Delete(Article art);
        void Update(Article art);
    }
}
