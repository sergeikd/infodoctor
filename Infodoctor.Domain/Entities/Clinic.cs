using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        //public Guid OwnershipId { get; set; }
        public virtual ICollection<CityAddress> CityAddresses { get; set; }
        //public virtual ICollection<ClinicProfile> ClinicProfiles { get; set; }
        public virtual ICollection<ClinicSpecialization> ClinicSpecializations { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }

    public class CityAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<ClinicPhone> ClinicPhones { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }

    public class ClinicPhone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public virtual CityAddress ClinicAddress { get; set; }
    }
}
