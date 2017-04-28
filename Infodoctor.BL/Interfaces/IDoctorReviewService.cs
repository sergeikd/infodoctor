using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IDoctorReviewService
    {
        IEnumerable<DtoDoctorReview> GetAllReviews();
        IEnumerable<DtoDoctorReview> GetReviewsByDoctorId(int id);
        DtoPagedDoctorReview GetPagedReviewByDoctorId(int id, int perPage, int numPage);
        DtoDoctorReview GetReviewById(int id);
        void Add(DtoDoctorReview dr);
        void Update(DtoDoctorReview dr);
        void Delete(int id);
    }
}
