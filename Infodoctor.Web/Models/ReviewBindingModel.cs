namespace Infodoctor.Web.Models
{
    public class ReviewBindingModel
    {
        public string Text { get; set; }
        public int RatePrice { get; set; }
        public int RateQuality { get; set; }
        public int RatePoliteness { get; set; }
    }

    public class ClinicReviewBindingModel : ReviewBindingModel
    {
        public int ClinicId { get; set; }
    }

    public class DoctorReviewBindingModel : ReviewBindingModel
    {
        public int DoctorId { get; set; }
    }
}