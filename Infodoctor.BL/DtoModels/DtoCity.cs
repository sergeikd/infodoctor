using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoCity
    {
        public int Id { get; set; }
        public List<LocalizedDtoCity> LocalizedDtoCity { get; set; }
    }

    public class LocalizedDtoCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }
}
