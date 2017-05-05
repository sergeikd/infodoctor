using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public string Manipulation { get; set; }
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
        public virtual DoctorSpecialization Specialization { get; set; }
        public virtual DoctorCategory Category { get; set; }
        public virtual ClinicAddress Address { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
        public virtual ICollection<DoctorReview> DoctorReviews { get; set; }
    }
}
