using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly ILanguageRepository _langRepository;

        public ArticlesService(IArticlesRepository articlesRepository, ILanguageRepository LangRepository)
        {
            if (articlesRepository == null)
                throw new ArgumentNullException(nameof(articlesRepository));
            if (LangRepository == null) throw new ArgumentNullException(nameof(LangRepository));
            _articlesRepository = articlesRepository;
            _langRepository = LangRepository;
        }

        public IEnumerable<DtoArticle> GetAllArticles()
        {
            var arts = _articlesRepository.GetAllArticles().ToList();
            return arts.Select(EntityToDto).ToList();
        }

        public DtoArticle GetArticleById(int id)
        {
            var art = _articlesRepository.GetArticleById(id);
            var dtoArt = EntityToDto(art);
            return dtoArt;
        }

        public DtoPagedArticles GetPagedArticles(int perPage, int numPage, string pathToImage)
        {
            if (perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var arts = _articlesRepository.GetAllArticles();

            var pagedList = new PagedList<Article>(arts, perPage, numPage);

            if (!pagedList.Any())
                return null;

            var dtoArts = pagedList.Select(EntityToDto).ToList();

            var paged = new DtoPagedArticles()
            {
                Articles = dtoArts,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount
            };

            return paged;
        }

        public void Add(DtoArticle art)
        {
            if (art == null)
                throw new ArgumentNullException(nameof(art));

            var lang = _langRepository.GetLanguageByCode(art.LangCode);

            var newArt = new Article()
            {
                Title = art.Title,
                Content = art.Content,
                PublishDate = art.PublishDate,
                Author = art.Author,
                Language = lang
            };

            _articlesRepository.Add(newArt);
        }

        public void Update(int id, DtoArticle newArt)
        {
            if (newArt == null)
                throw new ArgumentNullException(nameof(newArt));
            var updated = _articlesRepository.GetArticleById(id);
            if (updated == null) return;

            var lang = _langRepository.GetLanguageByCode(newArt.LangCode);

            updated.Language = lang;
            updated.Title = newArt.Title;
            updated.Content = newArt.Content;
            updated.PublishDate = newArt.PublishDate;

            _articlesRepository.Update(updated);
        }

        public void Delete(int id)
        {
            var deleted = _articlesRepository.GetArticleById(id);
            _articlesRepository.Delete(deleted);
        }

        private static DtoArticle EntityToDto(Article article)
        {
            var dtoComments = new List<DtoArticleComment>();
            if (article.Comments.Any())
                dtoComments.AddRange(
                    article.Comments.Select(
                        comment => new DtoArticleComment()
                        {
                            Id = comment.Id,
                            UserName = comment.UserName,
                            UserId = comment.UserId,
                            Text = comment.Text,
                            LangCode = comment.Language.Code.ToLower(),
                            PublishTime = comment.PublishTime,
                            ArticleId = comment.Article.Id
                        }));

            var dtoArt = new DtoArticle()
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishDate = article.PublishDate,
                Author = article.Author,
                LangCode = article.Language.Code.ToLower(),
                Comments = dtoComments.Select(x => x.Id).ToList()
            };
            return dtoArt;
        }
    }
}