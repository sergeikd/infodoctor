using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Interfaces
{
    public interface IClinicReviewService
    {
        IEnumerable<DtoClinicReview> GetClinicReviews();
        IEnumerable<DtoClinicReview> GetReviewsByClinicId(int id);
        DtoPagedClinicReview GetPagedReviewsByClinicId(int id, int perPage, int numPage);
        DtoClinicReview GetClinicReviewById(int id);
        void Add(DtoClinicReview clinicReview);
        void Update(ClinicReview clinicReview);
        void Delete(int id);
    }
}
