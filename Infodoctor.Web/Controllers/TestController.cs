using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure.Interfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : ApiController
    {
        private readonly ITestService _testService;
        private readonly IConfigService _configService;

        public TestController(ITestService testService, IConfigService configService)
        {
            if (testService == null)
            {
                throw new ArgumentNullException(nameof(testService));
            }
            if (configService == null)
            {
                throw new ArgumentNullException(nameof(configService));
            }
            _testService = testService;
            _configService = configService;
        }

        [HttpGet]
        [Route("api/test/add100clinics")]
        public IHttpActionResult Add100Clinics()
        {
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;
            _testService.Add100Clinics(pathToImage);
            return Ok();
        }
        [HttpGet]
        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
