using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.Web.Models;
using System.Web;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Domain.Entities;
using Infodoctor.Web.Infrastructure.Interfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ArticlesController : ApiController
    {
        private readonly IArticlesService _articlesService;
        private readonly IImagesService _imagesService;
        private readonly IConfigService _configService;

        public ArticlesController(IArticlesService articlesService, IConfigService configService, IImagesService imagesService)
        {
            if (articlesService == null)
                throw new ArgumentNullException(nameof(articlesService));
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));
            if (imagesService == null)
                throw new ArgumentNullException(nameof(imagesService));

            _imagesService = imagesService;
            _configService = configService;
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

        // GET: api/articles/page/perPage/numPage
        [AllowAnonymous]
        [Route("api/Articles/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedArticles GetPage(int perPage, int numPage)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            return _articlesService.GetPagedArticles(perPage, numPage, pathToImage);
        }

        // POST: api/articles/postimage
        [Authorize(Roles = "admin, moder")]
        [Route("api/Articles/PostImage")]
        [HttpPost]
        public string PostImage()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            var imageUrl = string.Empty;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToArticlesImages;
            if (file != null && file.ContentLength > 0)
                imageUrl = _imagesService.Add(file, _configService.PathToArticlesImages, pathToImage, 0);

            return imageUrl;
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
                Title = article.Title,
                Content = article.Content,
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
                Title = article.Title,
                Content = article.Content,
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