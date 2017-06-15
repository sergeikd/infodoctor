using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedCountry> LocalizedCountries { get; set; }
    }

    public class LocalizedCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}
