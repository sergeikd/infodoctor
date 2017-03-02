﻿using System;
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
        public List<DtoAddress> ClinicAddress { get; set; }
        //public IEnumerable<ClinicProfile> ClinicProfiles { get; set; }
        public List<string> ClinicSpecialization { get; set; }
    }

    public class DtoAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public List <DtoPhone> ClinicPhones { get;set;}
}

public class DtoPhone
{
    public string Desc { get; set; }
    public string Phone { get; set; }
}
}
