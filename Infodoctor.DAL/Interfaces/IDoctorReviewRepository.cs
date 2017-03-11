using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Interfaces
{
    public interface IDoctorReviewRepository
    {
        IQueryable<DoctorReview> GetAllDoctorReviews();
        IQueryable<DoctorReview> GetReviewsByDoctorId(int id);
        DoctorReview GetDoctorReviewById(int id);
        void Add(DoctorReview dr);
        void Update(DoctorReview dr);
        void Delete(DoctorReview dr);
    }
}
