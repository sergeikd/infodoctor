using System.Collections.Generic;

namespace Infodoctor.Web.Models
{
    public class PublicFastSearchModel
    {
        public string Lang { get; set; }
        public List<int> TypeId { get; set; }
        public string Text { get; set; }
    }
}