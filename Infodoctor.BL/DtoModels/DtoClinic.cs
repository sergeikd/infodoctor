using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinic
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        //public Guid OwnershipId { get; set; }
        public List<DtoAddress> ClinicAddress { get; set; }
        //public IEnumerable<ClinicProfile> ClinicProfiles { get; set; }
        public List<DtoClinicSpecialization> ClinicSpecialization { get; set; }
        public List<DtoDoctor> Doctors { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
    }

    public class DtoAddress
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public List <DtoPhone> ClinicPhones { get;set;}
}

public class DtoPhone
{
    public string Desc { get; set; }
    public string Phone { get; set; }
}
}
