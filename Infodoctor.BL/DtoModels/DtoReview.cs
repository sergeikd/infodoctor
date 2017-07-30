using System;

namespace Infodoctor.BL.DtoModels
{
    public class DtoReview
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string LangCode { get; set; }
        public DateTime PublishTime { get; set; }
    }

    public class DtoDoctorReview : DtoReview
    {
        public int DoctorId { get; set; }
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
    }

    public class DtoClinicReview : DtoReview
    {
        public int ClinicId { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
    }

    public class DtoResortReview : DtoReview
    {
        public int ResortId { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
    }

    public class DtoArticleComment : DtoReview
    {
        public int ArticleId { get; set; }
    }
}
