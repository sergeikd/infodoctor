using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class DoctorCategory
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedDoctorCategory> Localized { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }

    public class LocalizedDoctorCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}
