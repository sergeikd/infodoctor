using System.Collections.Generic;
using Infodoctor.BL.DtoModels;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Intefaces
{
    public interface IDoctorReviewService
    {
        IEnumerable<DoctorReview> GetAllReviews();
        IEnumerable<DoctorReview> GetReviewsByDoctorId(int id);
        DtoPagedDoctorReview GetPagedReviewByDoctorId(int id, int perPage, int numPage);
        DoctorReview GetReviewById(int id);
        void Add(DoctorReview dr);
        void Update(DoctorReview dr);
        void Delete(int id);
    }
}
