using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : ApiController
    {
        private readonly ITestService _testService;
        private readonly ConfigService _configService;

        public TestController(ITestService testService, ConfigService configService)
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
        [Route("api/test/add10clinics")]
        public IHttpActionResult Add100Clinics()
        {
            var pathToImage = _configService.PathToClinicsImages;
            _testService.Add10Clinics(pathToImage, _configService.ImagesSizes);
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
