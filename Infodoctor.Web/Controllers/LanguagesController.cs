using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.Web.Controllers
{
    public class LanguagesController : ApiController
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            if (languageService == null)
                throw new ArgumentNullException(nameof(languageService));
            _languageService = languageService;
        }

        // GET api/languages
        public IEnumerable<Language> Get()
        {
            return _languageService.GetLanguages();
        }

        // GET api/languages/5
        [HttpGet]
        [Route("api/languages/getbyid")]
        public Language GetById(int id)
        {
            return _languageService.GetLanguageById(id);
        }

        // GET api/languages/5
        [HttpGet]
        [Route("api/languages/getbycode")]
        public Language GetByCode(string code)
        {
            return _languageService.GetLanguageByCode(code);
        }

        // POST api/languages
        public void Post([FromBody]string value)
        {
        }

        // PUT api/languages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/languages/5
        public void Delete(int id)
        {
        }
    }
}
