using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public virtual ICollection<LocalisedCity> LocalisedCities { get; set; }
        public virtual ICollection<ClinicAddress> Adresses { get; set; }
    }

    public class LocalisedCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}
