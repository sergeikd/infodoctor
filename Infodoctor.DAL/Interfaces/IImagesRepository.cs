using Infodoctor.Domain;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IImagesRepository
    {
        IQueryable<ImageFile> GetAllImages();
        ImageFile GetImageDyId(int id);
        void Add(ImageFile artimg);
        void Update(ImageFile artimg);
        void Delete(ImageFile artimg);
    }
}
