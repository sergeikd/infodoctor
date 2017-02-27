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
        public IEnumerable<ClinicAddress> ClinicAddresses { get; set; }
        public IEnumerable<ClinicProfile> ClinicProfiles { get; set; }
        public IEnumerable<ClinicSpecialization> ClinicSpecializations { get; set; }
    }
}
