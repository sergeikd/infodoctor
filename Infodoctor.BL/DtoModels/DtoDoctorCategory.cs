using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctorCategorySingleLang
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
        public List<int> Doctors { get; set; }
    }

    public class DtoDoctorCategoryMultiLang
    {
        public int Id { get; set; }
        public List<DtoDoctorCategoryLocalized> Localized { get; set; }
        public List<int> Doctors { get; set; }
    }

    public class DtoDoctorCategoryLocalized
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
    }
}
