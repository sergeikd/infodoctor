using Infodoctor.Domain;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IArticleThemesRepository
    {
        IQueryable<ArticleTheme> GetAllThemes();
        ArticleTheme GetThemeById(int id);
        void Add(ArticleTheme artt);
        void Update(ArticleTheme artt);
        void Delete(ArticleTheme artt);
    }
}
