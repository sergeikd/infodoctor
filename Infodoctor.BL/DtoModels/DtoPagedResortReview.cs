using System;
using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoPagedResortReview
    {
        public IEnumerable<DtoResortReview> ResortReviews { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);  // total pages 
    }
}
