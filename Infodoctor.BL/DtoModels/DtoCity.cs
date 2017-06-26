using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    #region SingleLang
    public class DtoCitySingleLang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string LangCode { get; set; }
    }
    #endregion

    #region MultiLang
    public class DtoCityMultiLang
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public List<LocalizedDtoCity> LocalizedDtoCity { get; set; }
    }

    public class LocalizedDtoCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }
    #endregion
}
