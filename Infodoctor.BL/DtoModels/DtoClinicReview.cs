namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicReview : DtoReview
    {
        public int ClinicId { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
    }
}
