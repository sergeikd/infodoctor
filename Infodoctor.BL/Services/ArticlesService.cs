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
        private readonly ILanguageRepository _languageRepository;

        public ArticlesService(IArticlesRepository articlesRepository, ILanguageRepository languageRepository)
        {
            if (articlesRepository == null)
                throw new ArgumentNullException(nameof(articlesRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));
            _articlesRepository = articlesRepository;
            _languageRepository = languageRepository;
        }

        public IEnumerable<DtoArticle> GetAllArticles()
        {
            var arts = _articlesRepository.GetAllArticles().ToList();
            var dtoArts = new List<DtoArticle>();

            foreach (var art in arts)
            {
                var dtoComments = new List<DtoArticleComment>();
                if (art.Comments.Any())
                    foreach (var comment in art.Comments)
                    {
                        var dtoComment = new DtoArticleComment()
                        {
                            Id = comment.Id,
                            UserName = comment.UserName,
                            UserId = comment.UserId,
                            Text = comment.Text,
                            Lang = comment.Language.Code.ToLower(),
                            PublishTime = comment.PublishTime,
                            ArticleId = comment.Article.Id
                        };
                        dtoComments.Add(dtoComment);
                    }

                var dtoArt = new DtoArticle()
                {
                    Id = art.Id,
                    Title = art.Title,
                    Content = art.Content,
                    PublishDate = art.PublishDate,
                    Author = art.Author,
                    Language = art.Language.Code.ToLower(),
                    Comments = dtoComments
                };
                dtoArts.Add(dtoArt);
            }

            return dtoArts;
        }

        public DtoArticle GetArticleById(int id)
        {
            var art = _articlesRepository.GetArticleById(id);

            var dtoComments = new List<DtoArticleComment>();
            if (art.Comments.Any())
                foreach (var comment in art.Comments)
                {
                    var dtoComment = new DtoArticleComment()
                    {
                        Id = comment.Id,
                        UserName = comment.UserName,
                        UserId = comment.UserId,
                        Text = comment.Text,
                        Lang = comment.Language.Code.ToLower(),
                        PublishTime = comment.PublishTime,
                        ArticleId = comment.Article.Id
                    };
                    dtoComments.Add(dtoComment);
                }

            var dtoArt = new DtoArticle()
            {
                Id = art.Id,
                Title = art.Title,
                Content = art.Content,
                PublishDate = art.PublishDate,
                Author = art.Author,
                Language = art.Language.Code.ToLower(),
                Comments = dtoComments
            };
            return dtoArt;
        }

        public DtoPagedArticles GetPagedArticles(int perPage, int numPage, string pathToImage)
        {
            if (perPage < 1 || numPage < 1)
            {
                throw new ApplicationException("Incorrect request parameter");
            }
            var arts = _articlesRepository.GetAllArticles();
            var pagedList = new PagedList<Article>(arts, perPage, numPage);
            if (!pagedList.Any())
            {
                return null;
            }

            var dtoArts = new List<DtoArticle>();

            foreach (var art in pagedList)
            {
                var dtoComments = new List<DtoArticleComment>();
                if (art.Comments.Any())
                    foreach (var comment in art.Comments)
                    {
                        var dtoComment = new DtoArticleComment()
                        {
                            Id = comment.Id,
                            UserName = comment.UserName,
                            UserId = comment.UserId,
                            Text = comment.Text,
                            Lang = comment.Language.Code.ToLower(),
                            PublishTime = comment.PublishTime,
                            ArticleId = comment.Article.Id
                        };
                        dtoComments.Add(dtoComment);
                    }

                var dtoArt = new DtoArticle()
                {
                    Id = art.Id,
                    Title = art.Title,
                    Content = art.Content,
                    PublishDate = art.PublishDate,
                    Author = art.Author,
                    Language = art.Language.Code.ToLower(),
                    Comments = dtoComments
                };
                dtoArts.Add(dtoArt);
            }

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

            var lang = _languageRepository.GetLanguageByCode(art.Language);

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
            if (updated != null)
            {
                var lang = _languageRepository.GetLanguageByCode(newArt.Language);

                updated.Language = lang;
                updated.Title = newArt.Title;
                updated.Content = newArt.Content;
                updated.PublishDate = newArt.PublishDate;

                _articlesRepository.Update(updated);
            }
        }

        public void Delete(int id)
        {
            var deleted = _articlesRepository.GetArticleById(id);
            _articlesRepository.Delete(deleted);
        }
    }
}
