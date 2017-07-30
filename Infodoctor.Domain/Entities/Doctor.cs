using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infodoctor.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
        public bool Recommended { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FavouriteExpireDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RecommendedExpireDate { get; set; }
        public virtual DoctorCategory Category { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
        public virtual ICollection<DoctorReview> DoctorReviews { get; set; }
        public virtual ICollection<LocalizedDoctor> Localized { get; set; }
    }

    public class LocalizedDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manipulation { get; set; }
        public string Specialization { get; set; }
        public virtual Language Language { get; set; }
    }
}
