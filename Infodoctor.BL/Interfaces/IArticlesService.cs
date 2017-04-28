using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Interfaces
{
    public interface IArticlesService
    {
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int id);
        DtoPagedArticles GetPagedArticles(int perPage, int numPage, string pathToImage);
        void Add(Article art);
        void Update(int id,Article newArt);
        void Delete(int id);
    }
}
