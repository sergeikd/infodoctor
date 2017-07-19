using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoTypeSingleLang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }

    public class DtoClinicTypeSingleLang : DtoTypeSingleLang
    { }

    public class DtoResortTypeSingleLang : DtoTypeSingleLang
    { }

    public class DtoClinicTypeMultiLang
    {
        public int Id { get; set; }
        public List<DtoClinicTypeLocalized> Localized { get; set; }
        public List<int> ClinicsIdList { get; set; }
    }

    public class DtoResortTypeMultiLang
    {
        public int Id { get; set; }
        public List<DtoResortTypeLocalized> Localized { get; set; }
        public List<int> ResortsIdList { get; set; }
    }

    public class DtoTypeLocalized
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
    }

    public class DtoClinicTypeLocalized : DtoTypeLocalized
    { }

    public class DtoResortTypeLocalized : DtoTypeLocalized
    { }
}
