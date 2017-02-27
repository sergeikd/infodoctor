using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain;

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
        public void Post([FromBody]string title, string content)
        {
            var newArt = new Article()
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.Now
            };

            _articlesService.Add(newArt);
        }

        // PUT api/articles/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string title, string content)
        {
            var newArt = new Article()
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.Now
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