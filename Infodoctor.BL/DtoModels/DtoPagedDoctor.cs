using System;
using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoPagedDoctor
    {
        public List<DtoDoctorSingleLang> Doctors { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);  // total pages 
    }
}
