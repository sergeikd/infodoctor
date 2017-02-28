using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain;
using Infodoctor.Web.Models;
using System.Web;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ArticlesController : ApiController
    {
        private readonly IArticlesService _articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            if (articlesService == null)
                throw new ArgumentNullException(nameof(articlesService));
            _articlesService = articlesService;
        }

        // GET api/articles
        [AllowAnonymous]
        public IEnumerable<Article> Get()
        {
            return _articlesService.GetAllArticles();
        }

        // GET api/articles/5
        [AllowAnonymous]
        public Article Get(int id)
        {
            return _articlesService.GetArticleById(id);
        }

        // POST api/articles
        [Authorize(Roles = "admin, moder")]
        public void Post(PublicArticleModel article)
        {
            string userName = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity.Name != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }

            var newArt = new Article()
            {
                Title = article.title,
                Content = article.content,
                PublishDate = DateTime.Now,
                Author = userName
            };

            _articlesService.Add(newArt);
        }

        // PUT api/articles/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, PublicArticleModel article)
        {
            string userName = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity.Name != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }

            var newArt = new Article()
            {
                Title = article.title,
                Content = article.content,
                PublishDate = DateTime.Now,
                Author = userName
            };

            _articlesService.Update(id, newArt);
        }

        // DELETE api/articles/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _articlesService.Delete(id);
        }
    }
}