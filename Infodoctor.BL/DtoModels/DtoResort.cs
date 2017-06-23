﻿using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoResortSingleLang
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Specialisations { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
        public DtoAddressSingleLang Address { get; set; }
    }

    public class DtoResortMultiLang
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public List<DtoResortMLLocalizedP> Localized { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
        public DtoAddressMultiLang Address { get; set; }
    }

    public class DtoResortMLLocalizedP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialisations { get; set; }
    }
}
