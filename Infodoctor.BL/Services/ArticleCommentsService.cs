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
    public class ArticleCommentsService : IArticleCommentsService
    {
        private readonly IArticleCommentsRepository _commentsRepository;
        private readonly IArticlesRepository _articlesRepository;
        private readonly ILanguageRepository _languageRepository;

        public ArticleCommentsService(IArticleCommentsRepository commentsRepository, IArticlesRepository articlesRepository, ILanguageRepository languageRepository)
        {
            if (commentsRepository == null) throw new ArgumentNullException(nameof(commentsRepository));
            if (articlesRepository == null) throw new ArgumentNullException(nameof(articlesRepository));
            if (languageRepository == null) throw new ArgumentNullException(nameof(languageRepository));

            _commentsRepository = commentsRepository;
            _articlesRepository = articlesRepository;
            _languageRepository = languageRepository;
        }

        public IEnumerable<DtoArticleComment> GetComments()
        {
            var commentsList = _commentsRepository.GetAllArticleComments().ToList();
            var dtoCommentsList = new List<DtoArticleComment>();

            foreach (var comment in commentsList)
            {
                dtoCommentsList.Add(new DtoArticleComment()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    UserName = comment.UserName,
                    UserId = comment.UserId,
                    PublishTime = comment.PublishTime,
                    ArticleId = comment.Article.Id,
                    LangCode = comment.Language.Code.ToLower()
                });
            }

            return dtoCommentsList;
        }

        public IEnumerable<DtoArticleComment> GetCommentsByArticleId(int id)
        {
            var commentsList = _commentsRepository.GetCommentsByArticleId(id).ToList();
            var dtoCommentsList = new List<DtoArticleComment>();

            foreach (var comment in commentsList)
            {
                dtoCommentsList.Add(new DtoArticleComment()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    UserName = comment.UserName,
                    UserId = comment.UserId,
                    PublishTime = comment.PublishTime,
                    ArticleId = comment.Article.Id,
                    LangCode = comment.Language.Code.ToLower()
                });
            }

            return dtoCommentsList;
        }

        public DtoPagedArticleComments GetPagedCommentsByArticleId(int id, int perPage, int numPage)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            var commentsList = _commentsRepository.GetCommentsByArticleId(id).Where(c => c.IsApproved);

            var pagedList = new PagedList<ArticleComment>(commentsList, perPage, numPage);

            if (!pagedList.Any())
                throw new ApplicationException("Page not found");

            var dtoCommentsList = new List<DtoArticleComment>();

            foreach (var comment in pagedList)
            {
                dtoCommentsList.Add(new DtoArticleComment()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    UserName = comment.UserName,
                    UserId = comment.UserId,
                    PublishTime = comment.PublishTime,
                    ArticleId = comment.Article.Id,
                    LangCode = comment.Language.Code.ToLower()
                });
            }

            var result = new DtoPagedArticleComments
            {
                Comments = dtoCommentsList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };

            return result;
        }

        public DtoPagedArticleComments GetPagedCommentsByArticleId(int id, int perPage, int numPage, string lang)
        {
            if (id < 1 || perPage < 1 || numPage < 1)
                throw new ApplicationException("Incorrect request parameter");

            lang = lang.ToLower();

            var commentsList = _commentsRepository.GetCommentsByArticleId(id).Where(c => c.IsApproved)
                .OrderByDescending(c => c.Language.Code.ToLower() == lang)
                .ThenByDescending(c => c.Language.Code.ToLower() != lang)
                .ThenByDescending(c => c.PublishTime);

            var pagedList = new PagedList<ArticleComment>(commentsList, perPage, numPage);

            if (!pagedList.Any())
                throw new ApplicationException("Page not found");

            var dtoCommentsList = new List<DtoArticleComment>();

            foreach (var comment in pagedList)
            {
                dtoCommentsList.Add(new DtoArticleComment()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    UserName = comment.UserName,
                    UserId = comment.UserId,
                    PublishTime = comment.PublishTime,
                    ArticleId = comment.Article.Id,
                    LangCode = comment.Language.Code.ToLower()
                });
            }

            var result = new DtoPagedArticleComments
            {
                Comments = dtoCommentsList,
                TotalCount = pagedList.TotalCount,
                Page = pagedList.Page,
                PageSize = pagedList.PageSize
            };

            return result;
        }

        public DtoArticleComment GetCommentById(int id)
        {
            ArticleComment comment;

            try
            {
                comment = _commentsRepository.GetCommentById(id);
            }
            catch
            {
                throw new ApplicationException("Comment not found");
            }

            var dtoComment = new DtoArticleComment()
            {
                Id = comment.Id,
                Text = comment.Text,
                UserName = comment.UserName,
                UserId = comment.UserId,
                PublishTime = comment.PublishTime,
                ArticleId = comment.Article.Id,
                LangCode = comment.Language.Code.ToLower()
            };

            return dtoComment;
        }

        public void Add(DtoArticleComment comment)
        {
            if (comment.UserId == null || comment.UserName == null)
                throw new UnauthorizedAccessException("Incorrect user's credentials");

            Article article;

            try
            {
                article = _articlesRepository.GetArticleById(comment.ArticleId);
            }
            catch
            {
                throw new ApplicationException("Article not found");
            }

            if (comment.Text == "" || comment.ArticleId == 0 || comment.Text == string.Empty)
                throw new ApplicationException("Incorrect data, pussible some required fields are null or empty");

            var newComment = new ArticleComment()
            {
                Text = comment.Text,
                UserName = comment.UserName,
                UserId = comment.UserId,
                PublishTime = comment.PublishTime,
                Article = article,
                IsApproved = true, //TODO: change to false when moderator control will be done
                Language = _languageRepository.GetLanguageByCode(comment.LangCode)
            };

            _commentsRepository.Add(newComment);
        }

        public void Delete(int id)
        {
            ArticleComment articleComment;

            try
            {
                articleComment = _commentsRepository.GetCommentById(id);
            }
            catch
            {
                throw new ApplicationException("Comment not found");
            }

            if (articleComment != null)
                _commentsRepository.Delete(articleComment);
        }
    }
}
