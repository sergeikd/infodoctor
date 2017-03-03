using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Intefaces;
using Infodoctor.Web.Models;

namespace Infodoctor.Web.Controllers
{
    public class SearchController : ApiController
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService));
            _searchService = searchService;
        }

        // GET api/search
        private IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/search/5
        private string Get(int id)
        {
            return "value";
        }

        // POST api/search/fullsearch
        [Route("api/search/fullsearch")]
        [HttpPost]
        public PublicSearchResultModel FullSearch([FromBody]PublicSearchModel searchModel)
        {
            var result =
                _searchService.FullSeacrh(new DtoSearchModel()
                {
                    CityId = searchModel.CityId,
                    TypeId = searchModel.TypeId,
                    Text = searchModel.Text
                });
            var pubResult = new PublicSearchResultModel()
            {
                Clinics = result.Clinics,
                ClinicSpecializations = result.ClinicSpecializations
            };
            return pubResult;
        }

        // POST api/search/fastsearch
        [Route("api/search/fastsearch")]
        [HttpPost]
        public List<string> FastSearch([FromBody]PublicSearchModel searchModel)
        {
            var result =
                _searchService.FastSearch(new DtoSearchModel()
                {
                    CityId = searchModel.CityId,
                    TypeId = searchModel.TypeId,
                    Text = searchModel.Text
                });
            return result;
        }

        // PUT api/search/5
        private void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/search/5
        private void Delete(int id)
        {
        }
    }
}