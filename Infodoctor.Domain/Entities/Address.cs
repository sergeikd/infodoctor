﻿using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedCountry> LocalizedCountries { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedCity> LocalizedCities { get; set; }
        public virtual ICollection<Address> Adresses { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedAddress> LocalizedAddresses { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }

    public class Phone
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedPhone> LocalizedPhones { get; set; }
        public virtual Address Address { get; set; }
    }

    public class LocalizedCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }

    public class LocalizedCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }

    public class LocalizedAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public virtual City City { get; set; }
        public string Street { get; set; }
        public virtual Language Language { get; set; }
    }

    public class LocalizedPhone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public virtual Language Language { get; set; }
    }
}
