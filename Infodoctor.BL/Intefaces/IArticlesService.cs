using System.Collections.Generic;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Intefaces
{
    public interface IArticlesService
    {
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int id);
        void Add(Article art);
        void Update(int id,Article newArt);
        void Delete(int id);
    }
}
