using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using System;
using Infodoctor.Domain;
using System.Collections.Generic;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ArticleThemesService : IArticleThemesService
    {
        private readonly IArticleThemesRepository _articleThemesRepository;
        public ArticleThemesService(IArticleThemesRepository articleThemesRepository)
        {
            if (articleThemesRepository == null)
                throw new ArgumentNullException(nameof(articleThemesRepository));
            _articleThemesRepository = articleThemesRepository;
        }

        public IEnumerable<ArticleTheme> GetAllThemes()
        {
            return _articleThemesRepository.GetAllThemes();
        }

        public ArticleTheme GetThemeById(int id)
        {
            return _articleThemesRepository.GetThemeById(id);
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            var art = new ArticleTheme() { Name = name };
            if (IsNewElement(art))
                _articleThemesRepository.Add(art);
        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            var edited = _articleThemesRepository.GetThemeById(id);
            if (edited != null)
            {
                edited.Name = name;
                if (IsNewElement(edited))
                    _articleThemesRepository.Update(edited);
            }
        }

        public void Delete(int id)
        {
            var deleted = _articleThemesRepository.GetThemeById(id);
            if (deleted != null)
                _articleThemesRepository.Delete(deleted);
        }

        private bool IsNewElement(ArticleTheme art)
        {
            var artList = _articleThemesRepository.GetAllThemes();
            foreach (var element in artList)
                if (element.Name.ToUpper() == art.Name.ToUpper())
                    return false;
            return true;
        }
    }
}
