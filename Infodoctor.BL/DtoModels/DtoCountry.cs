using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> CitiesId { get; set; }
    }
}
