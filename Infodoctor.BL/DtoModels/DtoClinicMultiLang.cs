using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicMultiLang
    {
        public int Id { get; set; }
        public List<string> Images { get; set; }
        public List<LocalizedDtoClinic> LocalizedDtoClinics { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public List<DtoAddressMultiLang> ClinicAddress { get; set; }
        public List<DtoDoctorMultiLang> Doctors { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
    }

    public class DtoAddressMultiLang
    {
        public int Id { get; set; }
        public List<DtoPhoneMultiLang> Phones { get; set; }
        public List<LocalizedDtoAddress> LocalizedDtoAddresses { get; set; }
    }

    public class DtoPhoneMultiLang
    {
        public int Id { get; set; }
        public List<LocalizedDtoPhone> LocalizedDtoPhones { get; set; }
    }

    public class LocalizedDtoClinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string LangCode { get; set; }
    }

    public class LocalizedDtoAddress
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string LangCode { get; set; }
    }

    public class LocalizedDtoPhone
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Number { get; set; }
        public string LangCode { get; set; }
    }
}
