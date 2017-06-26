using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class Coords
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }

    #region SingleLang
    public class DtoAddressSingleLang
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Coords Coords { get; set; }
        public List<DtoPhoneSingleLang> Phones { get; set; }
    }

    public class DtoPhoneSingleLang
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Number { get; set; }
    }
    #endregion

    #region Multilang
    public class DtoAddressMultiLang
    {
        public int Id { get; set; }
        public Coords Coords { get; set; }
        public List<DtoPhoneMultiLang> Phones { get; set; }
        public List<LocalizedDtoAddress> LocalizedDtoAddresses { get; set; }
    }

    public class DtoPhoneMultiLang
    {
        public int Id { get; set; }
        public List<LocalizedDtoPhone> LocalizedDtoPhones { get; set; }
    }

    public class LocalizedDtoAddress
    {
        public int Id { get; set; }
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
    #endregion
}
