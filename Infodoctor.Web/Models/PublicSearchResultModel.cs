using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.Web.Models
{
    public class PublicSearchResultModel
    {
        //public PublicSearchModel SearchRequest { get; set; }
        public List<DtoClinicMultilang> Clinics { get; set; }
        public List<DtoClinicSpecializationMultilang> ClinicSpecializations { get; set; }
    }
}