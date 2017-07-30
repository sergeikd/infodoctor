using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IArticlesService
    {
        IEnumerable<DtoArticle> GetAllArticles();
        DtoArticle GetArticleById(int id);
        DtoPagedArticles GetPagedArticles(int perPage, int numPage, string pathToImage);
        void Add(DtoArticle art);
        void Update(int id, DtoArticle newArt);
        void Delete(int id);
    }
}
