using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class DoctorCategoryRepository : IDoctorCategoryRepository
    {
        private readonly IAppDbContext _context;

        public DoctorCategoryRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<DoctorCategory> GetAllCategories()
        {
            return _context.DoctorCategories;
        }

        public DoctorCategory GetCategoryById(int id)
        {
            return _context.DoctorCategories.First(dc => dc.Id == id);
        }

        public void Add(DoctorCategory category)
        {
            _context.DoctorCategories.Add(category);
            _context.SaveChanges();
        }

        public void Update(DoctorCategory category)
        {
            var updated = _context.DoctorCategories.First(dc => dc.Id == category.Id);
            updated = category;
            _context.SaveChanges();
        }

        public void Delete(DoctorCategory category)
        {
            _context.DoctorCategories.Remove(category);
            _context.SaveChanges();
        }
    }
}
