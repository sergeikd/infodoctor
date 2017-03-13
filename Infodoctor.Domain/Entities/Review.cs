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
        [Required]
        public double RatePrice { get; set; }
        [Required]
        public double RateQuality { get; set; }
        [Required]
        public double RatePoliteness { get; set; }
    }

    public class ClinicReview : Review
    {
        [Required]
        public int ClinicId { get; set; }
    }

    public class DoctorReview : Review
    {
        [Required]
        public int DoctorId { get; set; }
    }
}
