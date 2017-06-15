using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoCountrySingleLang
    {
        public string LangCode { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> CitiesId { get; set; }
    }

    public class DtoCountryMultiLang
    {
        public int Id { get; set; }
        public List<DtoCountryLocalized> LocalizedCoutries { get; set; }
        public List<int> CitiesId { get; set; }
    }

    public class DtoCountryLocalized
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }
}
