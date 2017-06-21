using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctorSingleLang
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Manipulation { get; set; }
        public string LangCode { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public DtoAddressSingleLang Address { get; set; }
        public string Specialization { get; set; }
        public DtoDoctorCategorySingleLang Category { get; set; }
        public List<int> ClinicsIds { get; set; } //сделано как List<int> для облегчения добавления клиники к доктору на фронте
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
    }

    public class DtoDoctorMultiLang
    {
        public int Id { get; set; }
        public string Image { get; set; }     
        public string Email { get; set; }
        public int Experience { get; set; }     
        public DtoAddressMultiLang Address { get; set; }
        public DtoDoctorCategoryMultiLang Category { get; set; }
        public List<int> ClinicsIds { get; set; } //сделано как List<int> для облегчения добавления клиники к доктору на фронте
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
        public List<DtoDoctorLocalized> Localized { get; set; }
    }

    public class DtoDoctorLocalized
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manipulation { get; set; }
        public string Specialization { get; set; }
        public string LangCode { get; set; }
    }
}
