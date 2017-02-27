using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IAppDbContext _context;

        public ImagesRepository(IAppDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<ImageFile> GetAllImages()
        {
            return _context.Images;
        }

        public ImageFile GetImageDyId(int id)
        {
            return _context.Images.First(img => img.Id == id);
        }

        public void Add(ImageFile artimg)
        {
            _context.Images.Add(artimg);
            _context.SaveChanges();
        }

        public void Update(ImageFile artimg)
        {
            var edited = _context.Images.First(img => img.Id == artimg.Id);
            edited = artimg;
        }

        public void Delete(ImageFile artimg)
        {
            _context.Images.Remove(artimg);
            _context.SaveChanges();
        }
    }
}
