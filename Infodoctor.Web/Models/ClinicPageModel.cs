using System;
using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.Web.Models
{
    public class ClinicPageModel
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);  // total pages
        public List<DtoClinicSingleLang> Clinics { get; set; } 
    }
}