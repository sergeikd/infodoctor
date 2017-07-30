using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool Childish { get; set; }
        public bool Favorite { get; set; }
        public bool Recommended { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FavouriteExpireDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RecommendedExpireDate { get; set; }
        public virtual ICollection<LocalizedClinic> Localized { get; set; }
        public virtual ICollection<ImageFile> ImageName { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<ClinicReview> ClinicReviews { get; set; }
        public virtual ClinicType Type { get; set; }
    }

    public class LocalizedClinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specializations { get; set; }
        public virtual Language Language { get; set; }
    }
}