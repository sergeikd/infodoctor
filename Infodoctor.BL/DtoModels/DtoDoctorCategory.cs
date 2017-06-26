using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    #region SinglLang
    public class DtoDoctorCategorySingleLang
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
        public List<int> DoctorsIdList { get; set; }
    }
    #endregion

    #region MultiLang
    public class DtoDoctorCategoryMultiLang
    {
        public int Id { get; set; }
        public List<DtoDoctorCategoryLocalized> LocalizedCategory { get; set; }
        public List<int> DoctorsIdList { get; set; }
    }

    public class DtoDoctorCategoryLocalized
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
    }
    #endregion
}
