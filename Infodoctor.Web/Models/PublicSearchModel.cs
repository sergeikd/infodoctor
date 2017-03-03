using System.Collections.Generic;

namespace Infodoctor.Web.Models
{
    public class PublicSearchModel
    {
        public List<int> TypeId { get; set; }
        public List<int> CityId { get; set; }
        public string Text { get; set; }
    }
}