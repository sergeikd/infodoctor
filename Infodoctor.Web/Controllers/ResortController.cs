using System;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ResortController : ApiController
    {
        private readonly IResortService _resort;
        private readonly ConfigService _config;

        public ResortController(IResortService resort, ConfigService config)
        {
            if (resort == null) throw new ArgumentNullException(nameof(resort));
            if (config == null) throw new ArgumentNullException(nameof(config));
            _resort = resort;
            _config = config;
        }

        // GET api/resort
        [AllowAnonymous]
        [Route("api/{lang}/Resort")]
        public IEnumerable<DtoResortSingleLang> Get(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _config.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetAllResorts(pathToImage, lang);
        }

        // GET api/resort/5
        [AllowAnonymous]
        [Route("api/{lang}/Resort")]
        public DtoResortSingleLang GetSingleLang(int id, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _config.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetResortById(id, pathToImage, lang);
        }

        // GET api/resort/5
        [AllowAnonymous]
        [Route("api/Resort")]
        public DtoResortMultiLang GetMultiLang(int id)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetResortById(id, pathToImage);
        }

        // GET: api/Resort/page/perPage/numPage
        [AllowAnonymous]
        [Route("api/{lang}/resort/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedResorts GetPage(int perPage, int numPage, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _config.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetPagedResorts(perPage, numPage, pathToImage, lang, 0);
        }

        // GET: api/Resort/page/perPage/numPage
        [AllowAnonymous]
        [Route("api/{lang}/resort/{type}/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedResorts GetPage(int type, int perPage, int numPage, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _config.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetPagedResorts(perPage, numPage, pathToImage, lang, type);
        }

        // api/Resort/search/perPage/numPage
        [AllowAnonymous]
        [Route("api/{lang}/resort/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedResorts SearchResort(int perPage, int numPage, [FromBody]DtoResortSearchModel searchModel, string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _config.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            var pagedResorts = _resort.SearchResorts(perPage, numPage, searchModel, pathToImage, lang, 0);

            return pagedResorts;
        }

        // api/Resort/search/perPage/numPage
        [AllowAnonymous]
        [Route("api/{lang}/resort/{type:int}/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedResorts SearchResort(int perPage, int numPage, [FromBody]DtoResortSearchModel searchModel, string lang, int type)
        {
            if (string.IsNullOrEmpty(lang))
                lang = _config.DefaultLangCode;

            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            var pagedResorts = _resort.SearchResorts(perPage, numPage, searchModel, pathToImage, lang, type);

            return pagedResorts;
        }

        // POST api/resort
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/resort/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/resort/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            _resort.Delete(id, pathToImage);
        }
    }
}