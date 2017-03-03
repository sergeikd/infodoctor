using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IClinicReviewRepository
    {
        IQueryable<ClinicReview> GetAllClinicReviews();
        IQueryable<ClinicReview> GetReviewsByClinicId(int id);
        ClinicReview GetClinicReviewById(int id);
        void Add(ClinicReview clinicReview);
        void Update(ClinicReview clinicReview);
        void Delete(ClinicReview clinicReview);
    }
}
