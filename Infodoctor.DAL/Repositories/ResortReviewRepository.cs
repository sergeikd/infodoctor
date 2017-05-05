using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ResortReviewRepository : IResortReviewRepository
    {
        private readonly IAppDbContext _context;

        public ResortReviewRepository(IAppDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<ResortReview> GetResortReviews()
        {
            return _context.ResortReviews.OrderBy(r => r.Id);
        }

        public IQueryable<ResortReview> GetResortReviewsByClinicId(int id)
        {
            return _context.ResortReviews.Where(r => r.Resort.Id == id).OrderBy(r => r.PublishTime);
        }

        public ResortReview GetReviewById(int id)
        {
            return _context.ResortReviews.First(r => r.Id == id);
        }

        public void Add(ResortReview rev)
        {
            _context.ResortReviews.Add(rev);
            _context.SaveChanges();
        }

        public void Update(ResortReview rev)
        {
            var updated = _context.ResortReviews.First(r => r.Id == rev.Id);
            updated = rev;
        }

        public void Remove(ResortReview rev)
        {
            _context.ResortReviews.Remove(rev);
            _context.SaveChanges();
        }
    }
}
