using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IResoteReviewRepository
    {
        IQueryable<ResortReview> GetResortReviews();
        IQueryable<ResortReview> GetResortReviewsByClinicId(int id);
        ResortReview GetReviewById(int id);
        void Add(ResortReview rev);
        void Update(ResortReview rev);
        void Remove(ResortReview rev);
    }
}
