namespace Infodoctor.BL.DtoModels
{
    public class DtoResort
    {
        public int Id { get; set; }
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
        public DtoAddressMultiLang AddressMultilang { get; set; }
    }
}
