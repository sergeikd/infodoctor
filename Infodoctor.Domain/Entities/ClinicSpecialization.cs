using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class ClinicSpecialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }
        public ICollection<Clinic> Clinics { get; set; }
    }
}
