using System.Collections.Generic;
using System.Drawing;
using System.Web;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface IImagesService
    {
        IEnumerable<DtoImage> GetAllImages();
        DtoImage GetImageById(int id);
        void Add(HttpPostedFile imageFile, string imageFolderPath, int maxImageWidth);
        void Update(int id, HttpPostedFile imageFile, string imageFolderPath, int maxImageWidth);
        void Delete(int id);
        bool ResizeImage(Image origImage, string fileName, string folderPath, int width);
    }
}
