using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoSearchModel
    {
        public string SearchWord { get; set; }
        public int CityId { get; set; }
        public string SortBy { get; set; }
        public string Descending { get; set; }
    }

    public class DtoClinicSearchModel : DtoSearchModel { }

    public class DtoDoctorSearchModel : DtoSearchModel { }

    public class DtoResortSearchModel : DtoSearchModel { }

    public class DtoFastSearchModel
    {
        public List<int> TypeId { get; set; }
        public string Text { get; set; }
        public string LangCode { get; set; }
    }
}
