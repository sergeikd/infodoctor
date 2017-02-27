using Infodoctor.Domain;
using System.Collections.Generic;
using System.Web;

namespace Infodoctor.BL.Intefaces
{
   public interface IImagesService
    {
        IEnumerable<ImageFile> GetAllImages();
        ImageFile GetImageById(int id);
        void Add(HttpPostedFileBase imageFile, string imageFolderPath, int maxImageWidth)
        void Update(HttpPostedFileBase imageFile, string imageFolderPath, int maxImageWidth)
        void Delete(int id);
        bool ResizeImage(ImageFile origImage, string fileName, string folderPath, int width);
    }
}
