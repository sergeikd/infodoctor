using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicReviewModel
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ClinicId { get; set; }
        public int RatePrice { get; set; }
        public int RateQuality { get; set; }
        public int RatePoliteness { get; set; }
        public DateTime PublishTime { get; set; }
        public bool IsApproved { get; set; }
    }
}
