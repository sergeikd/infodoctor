using System;
using System.Collections.Generic;
using Infodoctor.Domain;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public Guid OwnershipId { get; set; }
        public List<DtoClinicAddress> ClinicAddress { get; set; }
        //public IEnumerable<ClinicProfile> ClinicProfiles { get; set; }
        public List<string> ClinicSpecialization { get; set; }
    }

    public class DtoClinicAddress
    {
        public string ClinicAddress { get; set; }
        public List<Dictionary<string, string>> ClinicPhone { get; set; }
    }
}
