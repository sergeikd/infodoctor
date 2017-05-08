namespace Infodoctor.BL.DtoModels
{
    public class DtoResortReview: DtoReview
    {
        public int ResortId { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
    }
}
