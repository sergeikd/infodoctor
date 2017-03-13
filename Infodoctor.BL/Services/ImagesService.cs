using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IImagesRepository _imageRepository;

        public ImagesService(IImagesRepository imageRepository)
        {
            if (imageRepository == null)
                throw new ArgumentNullException(nameof(imageRepository));
            _imageRepository = imageRepository;
        }

        public IEnumerable<DtoImage> GetAllImages()
        {
            var imaged = _imageRepository.GetAllImages().ToList();
            var dtoImages = new List<DtoImage>();

            foreach (var img in imaged)
            {
                var dtoImg = new DtoImage()
                {
                    Id = img.Id,
                    Name = img.Name
                };

                dtoImages.Add(dtoImg);
            }

            return dtoImages;
        }

        public DtoImage GetImageById(int id)
        {
            var img = _imageRepository.GetImageDyId(id);
            var dtoImg = new DtoImage()
            {
                Id = img.Id,
                Name = img.Name
            };

            return dtoImg;
        }

        public void Add(HttpPostedFile imageFile, string imageFolderPath, int maxImageWidth)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var imgFileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".jpg";
            var filePath = AppDomain.CurrentDomain.BaseDirectory + imageFolderPath.Replace("~/", string.Empty).Replace("/", @"\") + imgFileName;
            var image = Image.FromStream(imageFile.InputStream, true, true);

            var result = ResizeImage(image, imgFileName, imageFolderPath, maxImageWidth);

            var img = new ImageFile() { Name = imgFileName, Path = filePath};

            if (result)
                _imageRepository.Add(img);
        }

        public void Update(int id, HttpPostedFile imageFile, string imageFolderPath, int maxImageWidth)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var img = _imageRepository.GetImageDyId(id);
            var image = Image.FromStream(imageFile.InputStream, true, true);

            var result = ResizeImage(image, img.Name, imageFolderPath, maxImageWidth);

            if (result)
                _imageRepository.Update(img);
        }

        public void Delete(int id)
        {
            var img = _imageRepository.GetImageDyId(id);
            File.Delete(img.Path);
            _imageRepository.Delete(img);
        }

        public bool ResizeImage(Image origImage, string fileName, string folderPath, int width)
        {
            var height = origImage.Height * width / origImage.Width; //calculate height of resized image
            folderPath = folderPath.Replace("~/", string.Empty).Replace("/", @"\");
            var filePath = AppDomain.CurrentDomain.BaseDirectory + folderPath + fileName;
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(origImage.HorizontalResolution, origImage.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(origImage, destRect, 0, 0, origImage.Width, origImage.Height, GraphicsUnit.Pixel,
                        wrapMode);
                }
            }
            try
            {
                destImage.Save(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
