using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ClinicAddress> Adresses { get; set; }
    }
}
