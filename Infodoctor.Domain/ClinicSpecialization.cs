using System.Collections.Generic;

namespace Infodoctor.Domain
{
    public class ClinicSpecialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }
}
