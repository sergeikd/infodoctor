namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctorReview : DtoReview
    {
        public int DoctorId { get; set; }
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
    }
}
