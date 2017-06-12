using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedCity> LocalizedCities { get; set; }
        public virtual ICollection<Address> Adresses { get; set; }
    }

    public class LocalizedCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}
