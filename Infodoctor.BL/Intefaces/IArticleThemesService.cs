using System.Collections.Generic;
using Infodoctor.Domain.Entities;

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
