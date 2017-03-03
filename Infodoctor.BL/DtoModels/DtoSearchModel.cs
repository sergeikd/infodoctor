using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoSearchModel
    {
        public List<int> TypeId { get; set; }
        public List<int> CityId { get; set; }
        public string Text { get; set; }
    }
}
