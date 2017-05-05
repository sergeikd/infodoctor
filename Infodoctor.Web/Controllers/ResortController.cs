using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ResortController : ApiController
    {
        private readonly IResortService _resort;
        private readonly IConfigService _config;

        public ResortController(IResortService resort, IConfigService config)
        {
            if (resort == null) throw new ArgumentNullException(nameof(resort));
            if (config == null) throw new ArgumentNullException(nameof(config));
            _resort = resort;
            _config = config;
        }

        // GET api/resort
        [AllowAnonymous]
        public IEnumerable<DtoResort> Get()
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetAllResorts(pathToImage);
        }

        // GET api/resort/5
        public DtoResort Get(int id)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetResortById(id, pathToImage);
        }

        // GET: api/Resort/page/perPage/numPage
        [AllowAnonymous]
        [Route("api/resort/page/{perPage:int}/{numPage:int}")]
        [HttpGet]
        public DtoPagedResorts GetPage(int perPage, int numPage)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            return _resort.GetPagedResorts(perPage, numPage, pathToImage);
        }

        // api/Resort/search/perPage/numPage
        [AllowAnonymous]
        [Route("api/resort/search/{perPage:int}/{numPage:int}")]
        [HttpPost]
        public DtoPagedResorts SearchClinic(int perPage, int numPage, [FromBody]DtoResortSearchModel searchModel)
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _config.PathToResortsImages;
            var pagedClinic = _resort.SearchResorts(perPage, numPage, searchModel, pathToImage);

            return pagedClinic;
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
        }
    }
}