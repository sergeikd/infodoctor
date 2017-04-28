using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;
using Infodoctor.Web.Infrastructure.Interfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class ImagesController : ApiController
    {
        private readonly IImagesService _imagesService;
        private readonly IConfigService _configService;

        public ImagesController(IImagesService imagesService, IConfigService configService)
        {
            if (imagesService == null)
                throw new ArgumentNullException(nameof(imagesService));
            if (configService == null)
                throw new ArgumentNullException(nameof(configService));

            _imagesService = imagesService;
            _configService = configService;
        }

        // GET api/images
        public IEnumerable<DtoImage> Get()
        {
            return _imagesService.GetAllImages();
        }

        // GET api/images/5
        public DtoImage Get(int id)
        {
            return _imagesService.GetImageById(id);
        }

        // POST api/images/doctors
        [Route("api/images/doctors")]
        [HttpPost]
        [Authorize(Roles = "admin, moder")]
        public void Doctors()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToDoctorsImages;

            if (file != null && file.ContentLength > 0)
                _imagesService.Add(file, _configService.PathToDoctorsImages, pathToImage, 400);
        }

        // POST api/images/clinics
        [Route("api/images/clinics")]
        [HttpPost]
        [Authorize(Roles = "admin, moder")]
        public void Clinics()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            var pathToImage = Request.RequestUri.GetLeftPart(UriPartial.Authority) + _configService.PathToClinicsImages;

            if (file != null && file.ContentLength > 0)
                _imagesService.Add(file, _configService.PathToClinicsImages, pathToImage, 400);
        }

        // PUT api/images/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/images/5
        public void Delete(int id)
        {
        }
    }
}