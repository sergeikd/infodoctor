using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoFastSearchModel
    {
        public List<int> TypeId { get; set; }
        public string Text { get; set; }
        public string LangCode { get; set; }
    }
}
