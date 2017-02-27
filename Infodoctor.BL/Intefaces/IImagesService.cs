using Infodoctor.Domain;
using System.Collections.Generic;
using System.Drawing;
using System.Web;

namespace Infodoctor.BL.Intefaces
{
   public interface IImagesService
    {
        IEnumerable<ImageFile> GetAllImages();
        ImageFile GetImageById(int id);
        void Add(HttpPostedFileBase imageFile, string imageFolderPath, int maxImageWidth);
        void Update(int id, HttpPostedFileBase imageFile, string imageFolderPath, int maxImageWidth);
        void Delete(int id);
        bool ResizeImage(Image origImage, string fileName, string folderPath, int width);
    }
}
