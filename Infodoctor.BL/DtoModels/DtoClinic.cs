using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    #region SingleLang
    public class DtoClinicSingleLang
    {
        public string LangCode { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Specializations { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public List<DtoAddressSingleLang> Addresses { get; set; }
        public List<int> DoctorsIdList { get; set; }
        public List<string> Images { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
    }
    #endregion

    #region Multilang
    public class DtoClinicMultiLang
    {
        public int Id { get; set; }
        public List<string> Images { get; set; }
        public List<LocalizedDtoClinic> LocalizedClinic { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public List<DtoAddressMultiLang> ClinicAddress { get; set; }
        public List<int> DoctorsIdList { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
    }

    public class LocalizedDtoClinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Specializations { get; set; }
        public string LangCode { get; set; }
    }
    #endregion
}
