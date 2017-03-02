using System.Collections.Generic;

namespace Infodoctor.Domain
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CityAddress> Addresses { get; set; }
    }
}
