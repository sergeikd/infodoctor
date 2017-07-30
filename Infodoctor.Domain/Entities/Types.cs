using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class ClinicType
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedClinicType> Localized { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }

    public class LocalizedClinicType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }

    public class ResortType
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedResortType> Localized { get; set; }
        public virtual ICollection<Resort> Resorts { get; set; }
    }

    public class LocalizedResortType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}