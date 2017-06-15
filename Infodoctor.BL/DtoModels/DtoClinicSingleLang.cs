using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicSingleLang
    {
        public string LangCode { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public List<DtoAddressSingleLang> Addresses { get; set; }
        public List<DtoClinicSpecializationSingleLangModels> Specializations { get; set; }
        public List<DtoDoctorMultiLang> Doctors { get; set; }
        public List<string> Images { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
    }

    public class DtoAddressSingleLang
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public List<DtoPhoneSingleLang> Phones { get; set; }
    }

    public class DtoPhoneSingleLang
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Number { get; set; }
    }
}
