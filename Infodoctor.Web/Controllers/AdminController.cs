using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : ApiController
    {
        private readonly ConfigService _configService;
        private readonly ClinicsCsvParser _clinicsCsvParser;
        private readonly ResortsCsvParser _resortsCsvParser;
        private readonly IClinicService _clinicService;
        private readonly IResortService _resortService;

        public AdminController(IClinicService clinicService, ConfigService configService, ClinicsCsvParser clinicsCsvParser, IResortService resortService, ResortsCsvParser resortsCsvParser)
        {
            if (clinicService == null) throw new ArgumentNullException(nameof(clinicService));
            if (configService == null) throw new ArgumentNullException(nameof(configService));
            if (clinicsCsvParser == null) throw new ArgumentNullException(nameof(clinicsCsvParser));
            if (resortService == null) throw new ArgumentNullException(nameof(resortService));
            _clinicService = clinicService;
            _configService = configService;
            _clinicsCsvParser = clinicsCsvParser;
            _resortService = resortService;
            _resortsCsvParser = resortsCsvParser;
        }

        [HttpPost]
        [Route("api/admin/RebuildClinicsFromCsv")]
        public void RebuildClinicsFromCsv()
        {
            var path = _configService.PathToOldDbClinics;
            _clinicService.AddMany(
                _clinicsCsvParser.Parse(
                    AppDomain.CurrentDomain.BaseDirectory + path,
                    AppDomain.CurrentDomain.BaseDirectory + _configService.PathToClinicSourceImages,
                    AppDomain.CurrentDomain.BaseDirectory + _configService.PathToClinicsImages
                    ));
        }

        [HttpPost]
        [Route("api/admin/DeleteClinicImagesCsv")]
        public void DeleteClinicImagesCsv()
        {
            var path = _configService.PathToClinicsImages;
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + path);
            var filesInfolder = dir.GetFiles($"from-csv-*").ToList();
            foreach (var fileInfo in filesInfolder)
                File.Delete(fileInfo.FullName);
        }

        [HttpPost]
        [Route("api/admin/RebuildResortsFromCsv")]
        public void RebuildResortsFromCsv()
        {
            var path = _configService.PathToOldDbClinics;
            _resortService.AddMany(
                _resortsCsvParser.Parse(
                    AppDomain.CurrentDomain.BaseDirectory + path,
                    AppDomain.CurrentDomain.BaseDirectory + _configService.PathToClinicSourceImages,
                    AppDomain.CurrentDomain.BaseDirectory + _configService.PathToResortsImages
                ));
        }

        [HttpPost]
        [Route("api/admin/DeleteResortsImagesCsv")]
        public void DeleteResortsImagesCsv()
        {
            var path = _configService.PathToResortsImages;
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + path);
            var filesInfolder = dir.GetFiles($"from-csv-*").ToList();
            foreach (var fileInfo in filesInfolder)
                File.Delete(fileInfo.FullName);
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}