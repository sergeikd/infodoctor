using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class ClinicProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }
}
