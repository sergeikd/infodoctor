using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;
using Infodoctor.Web.Models;
using Microsoft.AspNet.Identity;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ArticleCommentsController : ApiController
    {
        private readonly IArticleCommentsService _commentsService;
        private readonly IYandexTranslateService _translateService;
        private readonly ConfigService _configService;

        public ArticleCommentsController(IArticleCommentsService commentsService, ConfigService configService, IYandexTranslateService translateService)
        {
            if (commentsService == null) throw new ArgumentNullException(nameof(commentsService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            if (translateService == null) throw new ArgumentNullException(nameof(translateService));
            _commentsService = commentsService;
            _configService = configService;
            _translateService = translateService;
        }

        // GET api/ArticleComments
        [Authorize(Roles = "admin, moder")]
        public IEnumerable<DtoArticleComment> Get()
        {
            return _commentsService.GetComments();
        }

        // GET api/ArticleComments/5
        [Authorize(Roles = "admin, moder")]
        public DtoArticleComment Get(int id)
        {
            return _commentsService.GetCommentById(id);
        }

        // GET: api/ArticleComments/page/clinicId/perPage/numPage 
        [AllowAnonymous]
        [Route("api/{lang}/ArticleComments/page/{articleId:int}/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedArticleComments GetPaged(int articleId, int perPage, int numPage, string lang)
        {
            return _commentsService.GetPagedCommentsByArticleId(articleId, perPage, numPage, lang);
        }

        // POST api/ArticleComments
        public void Post([FromBody]ArticleCommentBindingModel comment)
        {
            if (comment == null)
                throw new ApplicationException("Incorrect comment object");

            var newComment = new DtoArticleComment()
            {
                Text = comment.Text,
                ArticleId = comment.ArticleId,
                PublishTime = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName()
            };

            var apiKey = _configService.YandexTranslateApiKey;
            var textLang = _translateService.DetectLang(apiKey, comment.Text);

            newComment.LangCode = textLang != null ? textLang.Lang : comment.LangCode;

            _commentsService.Add(newComment);
        }

        // DELETE api/ArticleComments/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _commentsService.Delete(id);
        }

        // PUT api/ArticleComments/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}