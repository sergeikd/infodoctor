using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
        public virtual ICollection<LocalizedClinic> Localized { get; set; }
        public virtual ICollection<ImageFile> ImageName { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<ClinicReview> ClinicReviews { get; set; }
    }

    public class LocalizedClinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specializations { get; set; }
        public virtual Language Language { get; set; }
    }
}