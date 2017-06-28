using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Models;
using Microsoft.AspNet.Identity;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ArticleCommentsController : ApiController
    {
        private readonly IArticleCommentsService _commentsService;

        public ArticleCommentsController(IArticleCommentsService commentsService)
        {
            if (commentsService == null) throw new ArgumentNullException(nameof(commentsService));
            _commentsService = commentsService;
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
        public DtoPagedArticleComments GetPaged(int articleId, int perPage, int numPage,string lang)
        {
            return _commentsService.GetPagedCommentsByArticleId(articleId, perPage, numPage,lang);
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