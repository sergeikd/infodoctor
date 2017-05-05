using System;
using System.ComponentModel.DataAnnotations;

namespace Infodoctor.Domain.Entities
{
    //TODO: extend Review class with bool IsApproved property when moderators will be able to check each review
    //TODO: make service for e-mail sending to all moderators whether new review posted
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime PublishTime { get; set; }
        public bool IsApproved { get; set; }
    }

    public class ClinicReview : Review
    {
        [Required]
        //public int ClinicId { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public virtual Clinic Clinic { get; set; }
    }

    public class ResortReview : Review
    {
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public virtual Resort Resort { get; set; }
    }

    public class DoctorReview : Review
    {
        [Required]
        //public int DoctorId { get; set; }
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

    public class ArticleComment : Review
    {
        public virtual Article Article { get; set; }
    }
}
