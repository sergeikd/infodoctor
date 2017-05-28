using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infodoctor.Domain.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
        public virtual ICollection<ImageFile> ImageName { get; set; }
        public virtual ICollection<ClinicAddress> CityAddresses { get; set; }
        public virtual ICollection<ClinicSpecialization> ClinicSpecializations { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<ClinicReview> ClinicReviews { get; set; }
    }

    public class ClinicAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public virtual City City { get; set; }
        public string Street { get; set; }
        public virtual ICollection<ClinicPhone> Phones { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }

    public class ClinicPhone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public virtual ClinicAddress Address { get; set; }
    }
}