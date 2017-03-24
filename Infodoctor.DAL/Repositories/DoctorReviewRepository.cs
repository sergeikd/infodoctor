using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class DoctorReviewRepository : IDoctorReviewRepository
    {
        private readonly IAppDbContext _context;

        public DoctorReviewRepository(IAppDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<DoctorReview> GetAllDoctorReviews()
        {
            return _context.DoctorReviews;
        }

        public IQueryable<DoctorReview> GetReviewsByDoctorId(int id)
        {
            return _context.DoctorReviews.Where(r => r.Doctor.Id == id).OrderByDescending(n => n.PublishTime);
        }

        public DoctorReview GetDoctorReviewById(int id)
        {
            return _context.DoctorReviews.First(r => r.Id == id);
        }

        public void Add(DoctorReview dr)
        {
            _context.DoctorReviews.Add(dr);
            _context.SaveChanges();
        }

        public void Update(DoctorReview dr)
        {
            var updated = _context.DoctorReviews.First(r => r.Id == dr.Id);
            updated = dr;
            _context.SaveChanges();
        }

        public void Delete(DoctorReview dr)
        {
            _context.DoctorReviews.Remove(dr);
            _context.SaveChanges();
        }
    }
}
