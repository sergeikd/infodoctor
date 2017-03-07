using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public string Manipulation { get; set; }
        public virtual DoctorSpecialization Specialization { get; set; }
        public virtual DoctorCategory Category { get; set; }
        public virtual CityAddress Address { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }
}
