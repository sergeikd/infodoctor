using System.Collections.Generic;

namespace Infodoctor.Domain
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public Guid OwnershipId { get; set; }
        public virtual ICollection<CityAddress> CityAddresses { get; set; }
        //public virtual ICollection<ClinicProfile> ClinicProfiles { get; set; }
        public virtual ICollection<ClinicSpecialization> ClinicSpecializations { get; set; }

        //[NotMapped]
        //public List<Guid> Branches { get; set; }
        //[NotMapped]
        //public Dictionary<string, string> Phones { get; set; } = new Dictionary<string, string>();     

        //[Obsolete("Only for EntityFramework")]
        //public string BranchesJson
        //{
        //    get
        //    {
        //        return Branches == null || !Branches.Any() ? null : JsonConvert.SerializeObject(Branches);
        //    }

        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //            Branches.Clear();
        //        else
        //            Branches = JsonConvert.DeserializeObject<List<Guid>>(value);
        //    }
        //}

        //[Obsolete("Only for EntityFramework")]
        //public string PhonesJson
        //{
        //    get
        //    {
        //        return Phones == null || !Phones.Any() ? null : JsonConvert.SerializeObject(Phones);
        //    }

        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //            Phones.Clear();
        //        else
        //            Phones = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
        //    }
        //}
    }

    public class CityAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<ClinicPhone> ClinicPhones { get; set; }
        public virtual Clinic Clinic { get; set; }
    }

    public class ClinicPhone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public virtual CityAddress ClinicAddress { get; set; }
    }
}
