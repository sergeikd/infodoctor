using Infodoctor.Domain;
using System.Collections.Generic;

namespace Infodoctor.BL.Intefaces
{
    public interface IArticleThemesService
    {
        IEnumerable<ArticleTheme> GetAllThemes();
        ArticleTheme GetThemeById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
