﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infodoctor.BL.DtoModels
{
    public class DtoPagedClinic
    {
        public List<DtoClinic> Clinics { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);  // total pages 
    }
}
