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
using Infodoctor.BL.Interfaces;

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
            var img = _imageRepository.GetImageById(id);
            var dtoImg = new DtoImage()
            {
                Id = img.Id,
                Name = img.Name
            };

            return dtoImg;
        }

        public string Add(HttpPostedFile imageFile, string imageFolderPath, string pathToImage, int maxImageWidth)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var imgFileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".jpg";
            var filePath = AppDomain.CurrentDomain.BaseDirectory + imageFolderPath.Replace("~/", string.Empty).Replace("/", @"\") + imgFileName;
            var image = Image.FromStream(imageFile.InputStream, true, true);

            bool result = false;

            if (maxImageWidth == 0)
                result = ResizeImage(image, imgFileName, imageFolderPath, image.Width, image.Height);
            else
                result = ResizeImage(image, imgFileName, imageFolderPath, maxImageWidth, image.Height);


            //var img = new ImageFile() { Name = imgFileName, Path = filePath };

            //if (result)
            //    _imageRepository.Add(img);

            return pathToImage + imgFileName;
        }

        public void Update(int id, HttpPostedFile imageFile, string imageFolderPath, int maxImageWidth)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var img = _imageRepository.GetImageById(id);
            var image = Image.FromStream(imageFile.InputStream, true, true);

            bool result = false;

            if (maxImageWidth == 0)
                result = ResizeImage(image, img.Name, imageFolderPath, image.Width, image.Height);
            else
                result = ResizeImage(image, img.Name, imageFolderPath, maxImageWidth, image.Height);

            if (result)
                _imageRepository.Update(img);
        }

        public void Delete(int id, string pathToImageFolder)
        {
            var img = _imageRepository.GetImageById(id);
            if (img != null)
            {
                var directoryInfo = new DirectoryInfo(pathToImageFolder);
                var filesInDir = directoryInfo.GetFiles(img.Name + "*.jpg*");//get files include all filename ends like "_medium" and "_small"

                foreach (var foundFile in filesInDir)
                {
                    File.Delete(pathToImageFolder + foundFile.Name);
                }
                _imageRepository.Delete(img);
            }

        }

        public Image GetResizedImage(Image sourceImage, int width, int height)
        {
            var requiredAspectRatio = (double) width / height;
            var imageAspectRatio = (double) sourceImage.Width / sourceImage.Height;
            Rectangle cropRect;
            if (imageAspectRatio > requiredAspectRatio) //crop with cut left and right sides of the source image
            {
                var widthNew = (int)(sourceImage.Height * requiredAspectRatio);
                cropRect = new Rectangle((sourceImage.Width - widthNew)/2, 0, widthNew, sourceImage.Height );
                sourceImage = CropImage(sourceImage, cropRect);
            }
            if (imageAspectRatio < requiredAspectRatio) //crop with cut top and bottom sides of the source image
            {
                var heightNew = (int) (sourceImage.Width / requiredAspectRatio);
                cropRect = new Rectangle(0, (int) ((sourceImage.Height - heightNew)/2), sourceImage.Width, heightNew);
                sourceImage = CropImage(sourceImage, cropRect);
            }

            //sourceImage.Save("c:\\testCrop.jpg", ImageFormat.Jpeg);//TODO delete it after test
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

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
                    graphics.DrawImage(sourceImage, destRect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel,
                        wrapMode);
                }
            }
            //destImage.Save("c:\\testResize.jpg", ImageFormat.Jpeg);//TODO delete it after test
            return destImage;
        }

        private static Bitmap CropImage(Image sourceImage, Rectangle cropRect)
        {
            var croppedImage = new Bitmap(cropRect.Width, cropRect.Height);
            using (var g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(sourceImage, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), cropRect, GraphicsUnit.Pixel);
            }
            return croppedImage;
        }

        public bool ResizeImage(Image origImage, string fileName, string folderPath, int width, int height)
        {
            //var height = origImage.Height * width / origImage.Width; //calculate height of resized image
            folderPath = folderPath.Replace("~/", string.Empty).Replace("/", @"\");
            var filePath = AppDomain.CurrentDomain.BaseDirectory + folderPath + fileName;
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(origImage.HorizontalResolution, origImage.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.High;
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
