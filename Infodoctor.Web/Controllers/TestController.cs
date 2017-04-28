using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Interfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : ApiController
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            if (testService == null) throw new ArgumentNullException(nameof(testService));
            _testService = testService;
        }

        [HttpGet]
        [Route("api/test/add100clinics")]
        public IHttpActionResult Add100Clinics()
        {
            _testService.Add100Clinics();
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
