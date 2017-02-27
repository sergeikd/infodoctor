using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using System;
using Infodoctor.Domain;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Infodoctor.BL.Services
{
    public class ImagesService : IImagesService
    {
        public readonly IImagesRepository _imageRepository;

        public ImagesService(IImagesRepository imageRepository)
        {
            if (imageRepository == null)
                throw new ArgumentNullException(nameof(imageRepository));
            _imageRepository = imageRepository;
        }

        public IEnumerable<ImageFile> GetAllImages()
        {
            return _imageRepository.GetAllImages();
        }

        public ImageFile GetImageById(int id)
        {
            return _imageRepository.GetImageDyId(id);
        }

        public void Add(HttpPostedFileBase imageFile, string imageFolderPath, int maxImageWidth)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var imgFileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".jpg";
            var filePath = AppDomain.CurrentDomain.BaseDirectory + imageFolderPath.Replace("~/", string.Empty).Replace("/", @"\") + imgFileName;
            var image = Image.FromStream(imageFile.InputStream, true, true);

            var result = ResizeImage(image, imgFileName, imageFolderPath, maxImageWidth);

            var img = new ImageFile() { Name = imgFileName, Path = filePath };

            if (result)
                _imageRepository.Add(img);
        }

        public void Update(int id, HttpPostedFileBase imageFile, string imageFolderPath, int maxImageWidth)
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
