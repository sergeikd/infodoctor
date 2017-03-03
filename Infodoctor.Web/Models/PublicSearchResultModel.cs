using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.Web.Models
{
    public class PublicSearchResultModel
    {
        //public PublicSearchModel SearchRequest { get; set; }
        public List<DtoClinic> Clinics { get; set; }
        public List<DtoClinicSpecialization> ClinicSpecializations { get; set; }
    }
}