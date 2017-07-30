using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IResortReviewService
    {
        IEnumerable<DtoResortReview> GetAllReviews();
        IEnumerable<DtoResortReview> GetReviewsByResortId(int id);
        DtoPagedResortReview GetPagedReviewByResortId(int id, int perPage, int numPage);
        DtoPagedResortReview GetPagedReviewByResortId(int id, int perPage, int numPage, string lang);
        DtoResortReview GetResortReviewById(int id);
        void Add(DtoResortReview review);
        void Update(DtoResortReview review);
        void Delete(int id);
    }
}
