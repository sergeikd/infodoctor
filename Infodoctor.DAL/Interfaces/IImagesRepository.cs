using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IImagesRepository
    {
        IQueryable<ImageFile> GetAllImages();
        ImageFile GetImageById(int id);
        ImageFile GetImageByName(string name);
        void Add(ImageFile artimg);
        void Update(ImageFile artimg);
        void Delete(ImageFile artimg);
    }
}
