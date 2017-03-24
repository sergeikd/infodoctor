using System;

namespace Infodoctor.BL.DtoModels
{
    public class DtoReview
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime PublishTime { get; set; }
    }
}
