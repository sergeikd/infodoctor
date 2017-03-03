using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Intefaces
{
    public interface IClinicReviewService
    {
        IEnumerable<ClinicReview> GetClinicReviews();
        IEnumerable<ClinicReview> GetReviewsByClinicId(int id);
        DtoPagedClinicReview GetPagedReviewsByClinicId(int id, int perPage, int numPage);
        ClinicReview GetClinicReviewById(int id);
        void Add(ClinicReview clinicReview);
        void Update(ClinicReview clinicReview);
        void Delete(int id);
    }
}
