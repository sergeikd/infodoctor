using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicReviewRepository : IClinicReviewRepository
    {
        private readonly AppDbContext _context;

        public ClinicReviewRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<ClinicReview> GetAllClinicReviews()
        {
            return _context.ClinicReviews;
        }

        public IQueryable<ClinicReview> GetReviewsByClinicId(int id)
        {
            return _context.ClinicReviews.Where(r => r.Clinic.Id == id).OrderByDescending(n => n.PublishTime);
        }

        public ClinicReview GetClinicReviewById(int id)
        {
            return _context.ClinicReviews.First(c => c.Id == id);
        }

        public void Add(ClinicReview clinicReview)
        {
            _context.ClinicReviews.Add(clinicReview);
            _context.SaveChanges();
        }

        public void Update(ClinicReview clinicReview)
        {
            var updated = _context.ClinicReviews.First(c => c.Id == clinicReview.Id);
            updated = clinicReview;
            _context.SaveChanges();
        }

        public void Delete(ClinicReview clinicReview)
        {
            _context.ClinicReviews.Remove(clinicReview);
            _context.SaveChanges();
        }
    }
}
