using System.Collections.Generic;

namespace Infodoctor.Web.Models
{
    public class CountryPostBindingModel
    {
        public string Name { get; set; }
        public List<int> CitiesId { get; set; }
    }
}