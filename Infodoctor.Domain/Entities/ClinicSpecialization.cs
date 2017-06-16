﻿using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class ClinicSpecialization
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedClinicSpecialization> LocalizedClinicSpecializations { get; set; }
        public virtual ICollection<DoctorSpecializationMultiLang> DoctorSpecializations { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }

    public class LocalizedClinicSpecialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}
