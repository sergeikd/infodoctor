using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly AppDbContext _context;

        public ImagesRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<ImageFile> GetAllImages()
        {
            return _context.Images;
        }

        public ImageFile GetImageById(int id)
        {
            return _context.Images.First(img => img.Id == id);
        }

        public ImageFile GetImageByName(string name)
        {
            return _context.Images.First(img => img.Name == name);
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
