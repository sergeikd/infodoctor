using System.Collections.Generic;
using System.Drawing;
using System.Web;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IImagesService
    {
        IEnumerable<DtoImage> GetAllImages();
        DtoImage GetImageById(int id);
        string Add(HttpPostedFile imageFile, string imageFolderPath, string pathToImage, int maxImageWidth);
        void Update(int id, HttpPostedFile imageFile, string imageFolderPath, int maxImageWidth);
        void Delete(int id, string pathToImage);
        bool ResizeImage(Image origImage, string fileName, string folderPath, int width, int height);
        Image GetResizedImage(Image sourceImage, int width, int height);
    }
}
