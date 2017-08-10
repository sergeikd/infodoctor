using System;
using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    #region SingleLang
    public class DtoResortSingleLang
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Image { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public List<string> Manipulations { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public DtoAddressSingleLang Address { get; set; }
        public bool Favorite { get; set; }
        public DateTime FavouriteExpireDate { get; set; }
        public bool Recommended { get; set; }
        public DateTime RecommendedExpireDate { get; set; }
    }
    #endregion

    #region MultiLang
    public class DtoResortMultiLang
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public List<LocalizedDtoResort> LocalizedResort { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public DtoAddressMultiLang Address { get; set; }
        public bool Favorite { get; set; }
        public DateTime FavouriteExpireDate { get; set; }
        public bool Recommended { get; set; }
        public DateTime RecommendedExpireDate { get; set; }
    }

    public class LocalizedDtoResort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Manipulations { get; set; }
        public string LangCode { get; set; }
    }
    #endregion
}
